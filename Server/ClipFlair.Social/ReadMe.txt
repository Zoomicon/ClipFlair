1 Introduction

This is a short introduction to MonoX, next generation portal development and social networking
framework for building advanced Web portals and applications. 
The complete user manual and API documentation can be found at the demo site, http://monox.mono-software.com.

MonoX provides tools for quick and intuitive construction of dynamic and fully editable portals and Web applications.
What You See Is What You Get (WYSIWYG) environment provided by MonoX engine allows users to
author advanced portals without coding. The first part of this manual is dedicated to explaining core
administrative concepts for all end user groups (administrators, designers, power users). The second
part - Portal development - describes advanced development tasks for users aiming to develop fully
customized solutions.

MonoX features include:
	- interactive user interface compatible with any recent Web browser, allowing for intuitive, drag-anddrop
	interaction with the portal elements. MonoX uses Ajax-based user interface elements and all
	the latest "Web 2.0" technologies to allow users to be as productive as possible.
	- full support for the ASP.NET Web parts framework: the Web Parts framework was one of the
	most conspicuous and the most powerful new features in Microsoft's ASP.NET 2.0. Basically, it is a
	set of controls and services that specifically target the growing demand for Web portal creation,
	including support for personalization, membership, communication and other infrastructural tasks.
	MonoX is built on top of the standard Web part API, so there is virtually no learning curve involved.
	- a fully featured set of components you need to build a advanced social environments:
	profiles, groups, friend lists, wall, photo albums, activity streams (event lists), video sharing,
	discussion lists, blogs, messaging, ...
	- W3C compliance: The W3C or World Wide Web Consortium is an international consortium
	dedicated to creating Web standards and guidelines. MonoX renders XHTML compliant code and
	enables users to develop fully standards-compliant portals that will work reliably on different
	platforms.
	- SEO optimization: MonoX includes powerful Search Engine Optimization mechanisms that can
	help users place their portals very high on all major search engines.
	- scalable and robust architecture: MonoX offers a scalable architecture that can satisfy the
	requirements of both small workgroups and large enterprises.
	- standardized plug-and-play modules and open API: new modules can be developed and
	plugged-in via standard Web part API.
	- high performance and flexible data layer: MonoX utilizes LLBLGen, a powerful object-relational
	engine that generates highly optimized, robust and scalable database-related code.
	- unprecedented ease of use: non-technical users can administer most of the aspects of the working
	portal. MonoX employs standard WYSIWYG editors, file manager mimics the look and feel of the
	Introduction 8
	Version 3.2.1754.35 - 09/21/2010 © 2008 - 2010 Mono LLC
	standard Windows explorer, etc. It fully supports offline editing tools based on MetaWeblog API
	(Microsoft Windows LiveWriter).

2 Installation

This section describes the prerequisites and the steps that are necessary to completely install the
MonoX portal framework.

2.1 Installation requirements

MonoX installation package requires:
	
	- Microsoft Windows 2003 Server or higher OR Microsoft XP Professional or higher, with IIS 5.1 or higher
	- Microsoft .NET 3.5 or higher
	- Microsoft SQL Server 2005 or higher OR Microsoft SQL Express

All commonly used browsers are supported in the end-user mode: Internet Explorer 7.0 or higher;
Firefox 1.0 or higher; Opera 7.5 or higher; Safari 1.2 or higher.
Internet Explorer and Firefox are also fully supported in the administration mode. Web part drag-and drop
operations are currently not supported on Firefox by Microsoft's Web part framework, but this does
not limit the design functionality since there are another means to move Web parts from one zone to
another.

2.2 Using the setup package

MonoX installation process consists of a few simple steps:
	
	- unzip the installation package in a folder of your choice (for example, C:\InetPub\wwwroot\MonoX).
	- create an empty database using the SQL Server Management Studio or similar management tool.
	Use the Security/Logins section of the Object explorer pane in the Management Studio to create a
	new login (or use the existing one) for the account that will be used by MonoX engine to access the
	database. Both Windows authentication and SQL Server authentication can be used, as long as you
	grant read, write and all ASP.NET-related privileges to this account using the User mapping section
	of the Login properties screen.
	- use the Internet Information Services management utility (type inetmgr in the Start-Run box) to
	create a new Web site or virtual folder for MonoX. If you are using a virtual folder, make sure that a
	new application is created (Application Settings section in the Directory tab of the folder properties
	screen). Please ensure that the application is placed in the correct application pool - two applications 
	compiled under different versions of .NET framework cannot run in the same app pool.

NOTE: By default, when ASP.NET allows anonymous access, the application runs in the context of a
special local account (this is NOT a domain account). In Windows 2000 and Windows XP, ASP.NET
applications run under the account called ASPNET. In Windows Server 2003, the account is called
NETWORK SERVICE. Windows 7 and Windows Server 2008 by default use ApplicationPoolIdentity. 
These user accounts are created during the .NET Framework installation process with a unique, strong password, 
and are granted only limited permissions.
MonoX installation wizard will try to set the write privileges on the folders that require it 
(/ApplicationData folder, web.config, etc.). 
This process may fail due to your local security configuration;
please make sure that the ASP.NET machine account has full write access on these folders (right click
on a folder using Windows Explorer, Properties, Security).
You can now access MonoX by typing its URL (as configured in the last step) in the browser window.
This will initiate the installation wizard, guiding you through the rest of the installation process.

2.3 Jump start a custom project

MonoX has a sample project that includes all neccessary references and folder structure, 
allowing you to jump start a custom project by adding a few pages and Web parts using Visual Studio, compiling it, 
and having a working portal in minutes. More complex projects will always be started this way. 
You can find the sample project in the "MonoX/Samples/Solution". To start using the sample project you need to copy 
the "ProjectName.sln" and "Portal.csproj" to the root of the web application.

2.4 Asp.Net 4.0 installation

Since version 4.0.2540 MonoX has a separate installation package for ASP.NET 4.0 and package can be found in standard MonoX 
download section at http://www.mono-software.com/Downloads/#MonoX.