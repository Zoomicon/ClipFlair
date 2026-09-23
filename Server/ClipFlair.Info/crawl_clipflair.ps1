$ErrorActionPreference = 'Stop'

$root = [Uri]'http://clipflair.net/'
$out = Join-Path (Get-Location) 'clipflair.net-mirror'
New-Item -ItemType Directory -Force -Path $out | Out-Null

function Get-ResourceFile([Uri]$uri, [string]$contentType = '') {
    $path = $uri.AbsolutePath.Trim('/')
    if ([string]::IsNullOrWhiteSpace($path)) { return 'index.html' }
    $safe = $path -replace '[^\w\-.\/]', '_'
    if ($safe.EndsWith('/')) { $safe = $safe.TrimEnd('/') }
    $extension = [IO.Path]::GetExtension($safe)
    if ([string]::IsNullOrWhiteSpace($extension) -and
        ([string]::IsNullOrWhiteSpace($contentType) -or $contentType -match 'html')) {
        return "$safe.html"
    }
    if ([string]::IsNullOrWhiteSpace($extension)) {
        $extension = switch -Regex ($contentType) {
            'css' { '.css'; break }
            'javascript' { '.js'; break }
            'json' { '.json'; break }
            'svg' { '.svg'; break }
            'xml' { '.xml'; break }
            default { '.bin' }
        }
        return "$safe$extension"
    }
    return $safe
}

function Get-LocalizedFile([Uri]$uri, [string]$contentType = '') {
    $file = Get-ResourceFile $uri $contentType
    if ($uri.AbsolutePath -match '^/(?<lang>[a-z]{2})(?:/|$)') {
        $prefix = "$($Matches.lang)/"
        if ($file.StartsWith($prefix, [StringComparison]::OrdinalIgnoreCase)) {
            $file = $file.Substring($prefix.Length)
        }
        return Join-Path $Matches.lang $file
    }
    return $file
}

function Normalize-Uri([Uri]$base, [string]$raw) {
    if ([string]::IsNullOrWhiteSpace($raw) -or $raw -match '^(?:#|data:|javascript:|mailto:|tel:|about:)') {
        return $null
    }
    try {
        $uri = [Uri]::new($base, ($raw -replace '&amp;', '&'))
        if ($uri.Scheme -notin @('http', 'https') -or $uri.Host -ine $root.Host) { return $null }
        return [UriBuilder]::new($uri) | ForEach-Object { $_.Fragment = ''; $_.Uri }
    } catch {
        return $null
    }
}

function Get-RelativeUrl([Uri]$from, [Uri]$to, [hashtable]$files) {
    $fromFile = Join-Path $out $files[$from.AbsoluteUri]
    $toFile = Join-Path $out $files[$to.AbsoluteUri]
    $relative = [IO.Path]::GetRelativePath((Split-Path $fromFile -Parent), $toFile)
    return $relative -replace '\\', '/'
}

$seedUrls = @(
    'http://clipflair.net/',
    'http://clipflair.net/overview/',
    'http://clipflair.net/aims-objectives/',
    'http://clipflair.net/conference2014/',
    'http://clipflair.net/consortium/',
    'http://clipflair.net/contact/',
    'http://clipflair.net/news/',
    'http://clipflair.net/outcomes/',
    'http://clipflair.net/privacy-policy/'
)

$queue = [Collections.Generic.Queue[Uri]]::new()
$seen = [Collections.Generic.HashSet[string]]::new()
$discovered = [Collections.Generic.HashSet[string]]::new()
$contentTypes = @{}
$resources = [Collections.Generic.List[object]]::new()
foreach ($seed in $seedUrls) {
    $seedUri = [Uri]$seed
    $queue.Enqueue($seedUri)
    $discovered.Add($seedUri.AbsoluteUri) | Out-Null
}

while ($queue.Count -gt 0) {
    $uri = $queue.Dequeue()
    if (-not $seen.Add($uri.AbsoluteUri)) { continue }
    $tempFile = [IO.Path]::GetTempFileName()
    try {
        $response = Invoke-WebRequest -Uri $uri.AbsoluteUri -UseBasicParsing -TimeoutSec 90 `
            -Headers @{ 'User-Agent' = 'ClipFlair offline mirror crawler' } -OutFile $tempFile -PassThru
        $bytes = [IO.File]::ReadAllBytes($tempFile)
        $type = [string]$response.Headers['Content-Type']
    } catch {
        Write-Warning "Failed $($uri.AbsoluteUri): $($_.Exception.Message)"
        continue
    } finally {
        Remove-Item $tempFile -Force -ErrorAction SilentlyContinue
    }

    $contentTypes[$uri.AbsoluteUri] = $type
    $resources.Add([pscustomobject]@{ Uri = $uri; Bytes = $bytes; ContentType = $type }) | Out-Null
    if ($type -match 'text/html|text/css|javascript|json|svg|xml') {
        $text = [Text.Encoding]::UTF8.GetString($bytes)
        $pattern = '(?is)(?<attribute>\b(?:href|src|action|poster)\s*=\s*)(?<quote>["''])(?<url>[^"'']+)\k<quote>|(?:url\(\s*["'']?(?<cssurl>[^)"'']+)["'']?\s*\))'
        foreach ($match in [regex]::Matches($text, $pattern)) {
            $raw = if ($match.Groups['url'].Success) { $match.Groups['url'].Value } else { $match.Groups['cssurl'].Value }
            $target = Normalize-Uri $uri $raw
            if ($null -ne $target) {
                $isPageLink = $match.Groups['attribute'].Value -match 'href|action' -and
                    $target.AbsolutePath -notmatch '\.[a-z0-9]{2,5}$'
                $isNewsListing = $uri.AbsolutePath -match '^/(?:news(?:/|$)|more-news(?:/|$)|[a-z]{2}/(?:news|more-news)(?:/|$))'
                $isArchivePage = $target.AbsolutePath -match '^/(?:tag|category)(?:/|$)'
                $isArchiveListing = $uri.AbsolutePath -match '^/(?:tag|category)(?:/|$)'
                $isWantedPage = $target.AbsolutePath -match '^/(?:conference2014(?:/|$)|more-news(?:/|$)|news(?:/|$)|outcomes(?:/|$)|tag(?:/|$)|category(?:/|$)|[a-z]{2}/(?:conference2014|more-news|news|outcomes|tag|category)(?:/|$))'
                if ($isNewsListing -and $isPageLink) {
                    $isWantedPage = $true
                }
                if ($isArchiveListing -and $isPageLink) {
                    $isWantedPage = $true
                }
                if ($isArchivePage -and $isPageLink) {
                    $isWantedPage = $true
                }
                if (-not $isPageLink -or $isWantedPage) {
                    $discovered.Add($target.AbsoluteUri) | Out-Null
                    $queue.Enqueue($target)
                }
            }
        }

        $languagePattern = '(?is)(?:["''](?<lang>en|el|es|ca|pl|ro|pt|eu|ga|et)["'']\s*:\s*["''](?<url>https?://[^"'']+)|hreflang\s*=\s*["''](?<lang2>en|el|es|ca|pl|ro|pt|eu|ga|et)["''][^>]+href\s*=\s*["''](?<url2>[^"'']+))'
        foreach ($match in [regex]::Matches($text, $languagePattern)) {
            $raw = if ($match.Groups['url'].Success) { $match.Groups['url'].Value } else { $match.Groups['url2'].Value }
            $target = Normalize-Uri $uri $raw
            if ($null -ne $target) {
                $discovered.Add($target.AbsoluteUri) | Out-Null
                $queue.Enqueue($target)
            }
        }
    }
}

$files = @{}
foreach ($uriString in $discovered) {
    $uri = [Uri]$uriString
    $type = if ($contentTypes.ContainsKey($uriString)) { $contentTypes[$uriString] } else { '' }
    $files[$uriString] = Get-LocalizedFile $uri $type
}

foreach ($resource in $resources) {
    $bytes = $resource.Bytes
    if ($resource.ContentType -match 'text/html|text/css|javascript|json|svg|xml') {
        $text = [Text.Encoding]::UTF8.GetString($bytes)
        $pattern = '(?is)(?<whole>(?<prefix>\b(?:href|src|action|poster)\s*=\s*)(?<quote>["''])(?<url>[^"'']+)(?:\k<quote>))|(?<whole>url\(\s*(?<quote>["'']?)(?<urlcss>[^)"'']+)(?:\k<quote>)\s*\))'
        $text = [regex]::Replace($text, $pattern, {
            param($match)
            $raw = if ($match.Groups['url'].Success) { $match.Groups['url'].Value } else { $match.Groups['urlcss'].Value }
            $target = Normalize-Uri $resource.Uri $raw
            if ($null -eq $target -or -not $files.ContainsKey($target.AbsoluteUri)) { return $match.Value }
            $local = Get-RelativeUrl $resource.Uri $target $files
            if ($match.Groups['url'].Success) {
                return $match.Value.Replace($raw, $local)
            }
            return $match.Value.Replace($raw, $local)
        })
        $absolutePattern = '(?i)https?://clipflair\.net(?<path>/[^"''<>\s]*)?'
        $text = [regex]::Replace($text, $absolutePattern, {
            param($match)
            $target = Normalize-Uri $resource.Uri $match.Value
            if ($null -eq $target -or -not $files.ContainsKey($target.AbsoluteUri)) { return $match.Value }
            return Get-RelativeUrl $resource.Uri $target $files
        })
        $bytes = [Text.Encoding]::UTF8.GetBytes($text)
    }

    $file = Join-Path $out $files[$resource.Uri.AbsoluteUri]
    New-Item -ItemType Directory -Force -Path (Split-Path $file -Parent) | Out-Null
    [IO.File]::WriteAllBytes($file, $bytes)
}

Write-Output "Saved $($resources.Count) same-host resources to $out"
