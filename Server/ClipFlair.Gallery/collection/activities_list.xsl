<?xml version="1.0" encoding="UTF-8"?>
<?altova_samplexml http://gallery.clipflair.net/collection/activities.cxml?>

<!--
Filename: activities_list.xsl
Version: 20140908
-->

<xsl:stylesheet 
version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:cxml="http://schemas.microsoft.com/collection/metadata/2009"
exclude-result-prefixes="cxml"
>

  <xsl:output method="html" version="4.0" encoding="UTF-8" indent="yes"/>
  <xsl:param name="COLUMNS" select="2"/>

  <!-- ########################### -->

  <xsl:template match="/">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>ClipFlair Activities</title>
        <link rel="stylesheet" type="text/css" href="List.css"/>
      </head>
      <body>
        <xsl:apply-templates/>
      </body>
    </html>
  </xsl:template>

  <!-- ########################### -->

  <xsl:template match="cxml:Collection">
    <div class="container">
      <xsl:apply-templates/>
    </div>
  </xsl:template>

  <!-- ########################### -->

  <xsl:template match="cxml:Items">
    <xsl:apply-templates select="cxml:Item[position() mod $COLUMNS = 1]" mode="row"/>
  </xsl:template>

  <!-- ########################### -->

  <xsl:template match="cxml:Item" mode="row">
    <div class="row">
      <xsl:apply-templates select=".|following-sibling::cxml:Item[position() &lt; $COLUMNS]" mode="col"/>
    </div>
  </xsl:template>

  <xsl:template match="cxml:Item" mode="col">
    <xsl:variable name="FILENAME" select="cxml:Facets/cxml:Facet[@Name='Filename']/cxml:String/@Value"/>
    <xsl:variable name="URL" select="@Href"/>
    <xsl:variable name="IMAGE" select="concat('http://gallery.clipflair.net/activity/image/', $FILENAME, '.png')"/>
    <xsl:variable name="TITLE" select="@Name"/>
    <xsl:variable name="DESCRIPTION" select="cxml:Description/text()" />
    <!-- Common Facets -->
    <xsl:variable name="AGE_GROUP" select="cxml:Facets/cxml:Facet[@Name='Age group']/cxml:String/@Value"/>
    <xsl:variable name="KEYWORDS" select="cxml:Facets/cxml:Facet[@Name='Keywords']/cxml:String/@Value"/>
    <xsl:variable name="AUTHORS_SOURCE" select="cxml:Facets/cxml:Facet[@Name='Authors / Source']/cxml:String/@Value"/>
    <xsl:variable name="FIRST_PUBLISHED" select="cxml:Facets/cxml:Facet[@Name='First published']/cxml:String/@Value"/>
    <xsl:variable name="LAST_UPDATED" select="cxml:Facets/cxml:Facet[@Name='Last updated']/cxml:String/@Value"/>
    <xsl:variable name="LICENSE" select="cxml:Facets/cxml:Facet[@Name='License']/cxml:String/@Value"/>
    <!-- Custom Facets -->
    <xsl:variable name="FOR_LEARNERS" select="cxml:Facets/cxml:Facet[@Name='For learners of']/cxml:String/@Value"/>
    <xsl:variable name="FOR_SPEAKERS" select="cxml:Facets/cxml:Facet[@Name='For speakers of']/cxml:String/@Value"/>
    <xsl:variable name="LEVEL" select="cxml:Facets/cxml:Facet[@Name='Level']/cxml:String/@Value"/>

    <div class="col">
    <h2 class="title"><a href="{$URL}" title="Open activity"><xsl:value-of select="$TITLE"/></a></h2>
    <div class="img-wrapper"><a href="{$URL}" title="Open activity"><img src="{$IMAGE}" /></a></div>
    <p class="description"><xsl:value-of select="$DESCRIPTION"/></p>
    <ul class="meta">
      <li><span class="meta-title">For learners of </span><xsl:value-of select="$FOR_LEARNERS"/></li>
      <li><span class="meta-title">For speakers of </span><xsl:value-of select="$FOR_SPEAKERS"/></li>
      <li><span class="meta-title">Level </span><xsl:value-of select="$LEVEL"/></li>
      <li><span class="meta-title">Author </span><xsl:value-of select="$AUTHORS_SOURCE"/></li>
    </ul>
    </div>
  </xsl:template>

  <!-- ########################### -->

  <xsl:template match="*">
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match="text()|@*">
  </xsl:template>

</xsl:stylesheet>
