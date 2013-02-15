UPDATE [Page] SET
	Url = '~/MonoXResources.aspx'
WHERE Url = '~/Resources.aspx'

UPDATE [aspnet_Paths] SET
	Path = '~/MonoXResources.aspx',
	LoweredPath = '~/monoxresources.aspx'
WHERE Path = '~/Resources.aspx'

UPDATE [aspnet_Paths] SET
	Path = '~/language/hr-HR/MonoXResources.aspx',
	LoweredPath = '~/language/hr-hr/monoxresources.aspx'
WHERE Path = '~/language/hr-HR/Resources.aspx'