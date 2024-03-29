/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO


SET IDENTITY_INSERT [dbo].[BlogPost] ON 
SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[BlogPost]';
DELETE FROM [dbo].[BlogPost];
GO

PRINT N'Inserting values into [dbo].[BlogPost]';

INSERT INTO [dbo].[BlogPost] ([Id],[IdentityId],[BlogId],[Title],[Description],[PostContent],[DateCreated],[DateModified],[DatePublished],[IsCommentEnabled],[Raters],[Rating],[Slug],[UserId],[IsPublished]) VALUES ('4D807944-37F8-4593-83BE-9C0D00E92DEF',1,'A81970C2-282F-42C5-92E4-9CF300F52FFC',N'Welcome to MonoX blog!',N'',N'This is a preview of the MonoX blog module. It is very easy to use, but still offers a complete functionality of &quot;stand-alone&quot; blog engines:<br/>
<ul>
    <li' +
    N'>multi-user support</li>
    <li>WYSIWYG editor</li>
    <li>support for multiple categories</li>
    <li>role support</li>
    <li>template/theme support</li>
    <li>automatic tag extraction</li>
    <li>support for ping services</li>
    <li>Search engine Optimization (SEO) support</li>
    <li>support for gravatars</li>
    <li>... and, of course, full integration with MonoX portal engine. </li>
</ul>
We will publish all updates on MonoX and the blog module in our regula' +
    N'r column on Mono Software main site, <a href="http://www.mono-software.com/Blog">http://www.mono-software.com/Blog</a>.<br/>
','2009-05-18T12:44:05.000','2009-05-18T12:44:26.000','2010-03-08T18:10:41.000',0,1,5,N'Welcome-to-MonoX-blog','67C919E2-8DF4-476A-B312-C26F82A36CFB',1);
GO
INSERT INTO [dbo].[BlogPost] ([Id],[IdentityId],[BlogId],[Title],[Description],[PostContent],[DateCreated],[DateModified],[DatePublished],[IsCommentEnabled],[Raters],[Rating],[Slug],[UserId],[IsPublished]) VALUES ('A97FA8A6-B61E-4210-8D45-9D9E0160CB41',4,'A81970C2-282F-42C5-92E4-9CF300F52FFC',N'Web Application Optimization',N'',N'Recently i started to use an application optimization package - <a href="http://wao.mono-software.com/">Web Application Optimizer (WAO)</a>
- with features that in' +
    N'clude HTTP compression, ViewState optimization
and CSS/Javascript optimization. WAO significantly increases the
response time for Web applications of all sizes, and, most importantly,
there is no need to modify the source code of your applications! <br />
<br />
The
package works in very different application scenarios and we''ve
got&nbsp;plenty of&nbsp;useful feedback from the first group of users. There has
been a slight confusion related to the availability of WAO in our Mon' +
    N'oX
framework. I wanted to formally announce that &nbsp;WAO is available by
default in all versions of MonoX. It is easy to tell if the WAO is
active by the absence of big ViewState&nbsp;data chunk from the page source.
','2010-06-23T19:19:47.000','2010-06-23T19:19:47.000','2010-06-23T19:19:47.000',1,0,0,N'Web-Application-Optimization','90F4FA98-E91A-430A-9916-F3EC5C93214E',1);
GO
INSERT INTO [dbo].[BlogPost] ([Id],[IdentityId],[BlogId],[Title],[Description],[PostContent],[DateCreated],[DateModified],[DatePublished],[IsCommentEnabled],[Raters],[Rating],[Slug],[UserId],[IsPublished]) VALUES ('C1379805-4D03-44D0-867B-9D9E01629406',5,'A81970C2-282F-42C5-92E4-9CF300F52FFC',N'MonoX as a Social Networking platform',N'',N'','2010-06-23T19:22:42.000','2010-06-23T19:22:42.000','2010-06-23T19:22:42.000',1,0,0,N'MonoX-as-a-Social-Networking-platform','A117436D-0970-4396-BF02-93DA7BFF6522',
    1);
DECLARE @rowID uniqueidentifier;
SELECT @rowID= [Id] FROM [dbo].[BlogPost] WHERE ([Id] = 'C1379805-4D03-44D0-867B-9D9E01629406');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'You may noticed that we''ve been quiet for the last couple of months. The
vacation season is not the primary reason for the lack of updates -
quite contrary, we are adding new features to MonoX on a daily basis.
Just to give you a test of things to come, here is a short list of
features that will be included in the next version that will be released
later this year: <br />
<br />
- MonoX is switching to&nbsp;.NET 3.5, allowing', 0, 436) WHERE ([Id] = 'C1379805-4D03-44D0-867B-9D9E01629406');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N' us
to use LINQ, Dynamic Data, portions of MVC technology and similar
stuff&nbsp;in the core libraries<br />
- we have switched to the Web application
project model, as it offers way better development experience on larger
projects - compilation process is now shortened&nbsp;dramatically<br />
- we
have introduced some exciting changes to the licensing scheme - more on
that later<br />
- MonoX will include a complete Social',     436, 436) WHERE ([Id] = 'C1379805-4D03-44D0-867B-9D9E01629406');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N' Networking module
set: friend lists, invitations, messaging, wall, notes, groups,
profiles... <br />
Stay tuned for more news and let us know which features
do you want to see included in next releases.
',     872, 209) WHERE ([Id] = 'C1379805-4D03-44D0-867B-9D9E01629406');
GO
GO
INSERT INTO [dbo].[BlogPost] ([Id],[IdentityId],[BlogId],[Title],[Description],[PostContent],[DateCreated],[DateModified],[DatePublished],[IsCommentEnabled],[Raters],[Rating],[Slug],[UserId],[IsPublished]) VALUES ('C9C46C9C-A976-4421-AFEA-9D9E0165757F',6,'A81970C2-282F-42C5-92E4-9CF300F52FFC',N'Fighting spam with MonoX',N'',N'','2010-06-23T19:32:03.000','2010-06-23T19:32:03.000','2010-06-23T19:32:03.000',1,0,0,N'Fighting-spam-with-MonoX','8F620F92-BCAE-4DF7-8A18-6B8942202C93',1);
DECLARE @rowID uniqueidentifier;
SELECT @rowID= [Id] FROM [dbo].[BlogPost] WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'Blog comment spam has been a massive frustration for our users over the
last few months. Although the original release of MonoX blog subsystem
followed the <a href="http://googleblog.blogspot.com/2005/01/preventing-comment-spam.html" target="_blank">Google recommendations for preventing comment spam</a>,
the guys behind this obviously lucrative business don''t really care
about "nofollow" tags - they are submitting non-relevant c', 0, 436) WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'omments
agresivelly. On a side note, there are several companies that allow you
to buy spam comments in batches of 100 or 1000, as described <a href="http://www.problogger.net/archives/2007/07/09/buy-blog-comments-a-sick-new-comment-spam-service-launches/" target="_blank">in this article</a>. It''s no wonder that some of our
users got more than 20k spam comments submitted in less than six months
to only a dozen&nbsp;blog posts.<b',     436, 436) WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'r />
<br />
This is going to change with the new,
"social networking"&nbsp;release of MonoX later this month. We provide a
pluggable, provider-based architecture that enables users to pick one or
more public antispam services or develop their own solutions.&nbsp;<a href="http://akismet.com/" target="_blank">Akismet</a> and&nbsp;<a href="http://defensio.com/" target="_blank">Defensio</a> are suported
out-of-the-box; if you want',     872, 436) WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N' to develop your own filter we recommend
starting with&nbsp;the article on&nbsp;<a href="http://www.codeproject.com/KB/recipes/BayesianCS.aspx" target="_blank">Bayesian spam filter</a>&nbsp;on CodeProject.<br />
<br />
You
may&nbsp;choose manual&nbsp;or automatic comment approval modes - in any case, if
the comment is considered to be spam, it will not show in the comment
list without your approval and your resident spammer wi',    1308, 436) WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'll have to search
for his next victim somewhere else.
',    1744, 56) WHERE ([Id] = 'C9C46C9C-A976-4421-AFEA-9D9E0165757F');
GO
GO
INSERT INTO [dbo].[BlogPost] ([Id],[IdentityId],[BlogId],[Title],[Description],[PostContent],[DateCreated],[DateModified],[DatePublished],[IsCommentEnabled],[Raters],[Rating],[Slug],[UserId],[IsPublished]) VALUES ('67E5FEE2-59D8-4428-BE77-9D9E016608E6',7,'A81970C2-282F-42C5-92E4-9CF300F52FFC',N'Cloud storage for your data',N'',N'','2010-06-23T19:34:27.000','2010-06-23T19:34:27.000','2010-06-23T19:34:27.000',1,0,0,N'Cloud-storage-for-your-data','A117436D-0970-4396-BF02-93DA7BFF6522',1);
DECLARE @rowID uniqueidentifier;
SELECT @rowID= [Id] FROM [dbo].[BlogPost] WHERE ([Id] = '67E5FEE2-59D8-4428-BE77-9D9E016608E6');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'With global workforces and customers spread across the globe,
organizations need to provide scalable tools for providing massive
amounts of&nbsp;information easily, quickly and securely. However, most of
our partners can''t afford, or simply don''t want, to build huge data
centers across the world to support this&nbsp;requirement. <br />
This is where&nbsp;<a href="http://aws.amazon.com/s3/" title="Amazon S3" class="ApplyClass" t', 0, 436) WHERE ([Id] = '67E5FEE2-59D8-4428-BE77-9D9E016608E6');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'arget="_blank">Amazon S3</a> steps in:&nbsp;it provides a simple Web
services interface that can be used to store and retrieve any amount of
data, at any time, from anywhere on the Web. It gives any developer
access to the same highly scalable, reliable, fast, and relatively
inexpensive data storage infrastructure that Amazon uses to run its own
global network of Web sites. <br />
MonoX now includes a data provider&nbsp;that
',     436, 436) WHERE ([Id] = '67E5FEE2-59D8-4428-BE77-9D9E016608E6');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'hooks into Amazon S3, allowing&nbsp;users to store their data in the cloud
and not on a database or file server somewhere in house. Best of all,
this functionality is totally transparent to the end user, and site
admins can access it via the familiar file explorer interface that is
identical to the standard GUI. Additionally, power users can develop
their own file providers for other storage services by extending the
FileBrows',     872, 436) WHERE ([Id] = '67E5FEE2-59D8-4428-BE77-9D9E016608E6');
UPDATE [dbo].[BlogPost] SET [PostContent] .WRITE (N'erContentProvider class and implementing the
IFileContentProvider interface.
',    1308, 79) WHERE ([Id] = '67E5FEE2-59D8-4428-BE77-9D9E016608E6');
GO
GO
GO

SET NOCOUNT OFF
SET IDENTITY_INSERT [dbo].[BlogPost] OFF
/* ======================================================================= */

PRINT N'Done.';

