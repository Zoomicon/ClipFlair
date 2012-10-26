SELECT     ls.Source, ls.ResourceKey, lss.Data, l.CultureName
FROM         LocalizationSource ls
inner join LocalizationString lss on lss.id = ls.id
inner join Language l on l.id = ls.languageid
where ls.Source like '%DualList%'