//Project: ClipFlair.Gallery (http://ClipFlair.codeplex.com)
//Filename: Login.aspx.cs
//Version: 20160616

using System;
using System.Web.Security;

namespace ClipFlair.Gallery
{
  public partial class Login : System.Web.UI.Page
  {

    #region --- Initialization ---

    protected void Page_Load(object sender, EventArgs e)
    {
      lblUserCount.Text = Membership.GetNumberOfUsersOnline().ToString();
    }

    #endregion

    protected void loginControl_LoggedIn(object sender, EventArgs e)
    {
      //note: following are not needed in normal scenaria

      //FormsAuthentication.SetAuthCookie(loginControl.UserName, loginControl.RememberMeSet);
      //FormsAuthentication.RedirectFromLoginPage(loginControl.UserName, loginControl.RememberMeSet);
    }

  }
}