﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
Project: ClipFlair (http://ClipFlair.codeplex.com)
Filename: ClipFlairPlayground.aspx
Version: 20130118
-->

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

  <title>ClipFlair Playground</title>

  <style type="text/css">
    html, body
    {
      height: 100%;
      overflow: hidden;
    }
    body
    {
      padding: 0;
      margin: 0;
    }
    #silverlightControlHost
    {
      height: 100%;
      text-align: center;
    }
  </style>

  <script type="text/javascript" src="Silverlight.js"></script>

  <script type="text/javascript">

    function onSilverlightError(sender, args) {
      var appSource = "";
      if (sender != null && sender != 0) {
        appSource = sender.getHost().Source;
      }

      var errorType = args.ErrorType;
      var iErrorCode = args.ErrorCode;

      if (errorType == "ImageError" || errorType == "MediaError") {
        return;
      }

      var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

      errMsg += "Code: " + iErrorCode + "    \n";
      errMsg += "Category: " + errorType + "       \n";
      errMsg += "Message: " + args.ErrorMessage + "     \n";

      if (errorType == "ParserError") {
        errMsg += "File: " + args.xamlFile + "     \n";
        errMsg += "Line: " + args.lineNumber + "     \n";
        errMsg += "Position: " + args.charPosition + "     \n";
      }
      else if (errorType == "RuntimeError") {
        if (args.lineNumber != 0) {
          errMsg += "Line: " + args.lineNumber + "     \n";
          errMsg += "Position: " + args.charPosition + "     \n";
        }
        errMsg += "MethodName: " + args.methodName + "     \n";
      }

      throw new Error(errMsg);
    }

    function onSilverlightLoad(sender, args) {
      var control = document.getElementById("silverlightControl");
      control.focus();
    }

    function activity() {
      var control = document.getElementById("silverlightControl");
      return control.content.activity;
    }

    function onClosing() {
      if (activity().View.WarnOnClosing)
        return "Are you sure you want to close ClipFlair Playground?";
    }

    function onClosed() {
      activity().View.WarnOnClosing = false;
    }

    function installEventHandlers() {
      window.onbeforeunload = onClosing;
      window.onunload = onClosed;
    }

    installEventHandlers();

  </script>

</head>

<body>

  <form id="form1" runat="server" style="width:100%; height:100%">
    <div id="silverlightControlHost">
      <object 
        id="silverlightControl"
        data="data:application/x-silverlight-2,"
        type="application/x-silverlight-2"
        width="100%" height="100%"
        >
        <param name="source" value="ClientBin/ClipFlair.xap" />
        <param name="onError" value="onSilverlightError" />
        <param name="onLoad" value="onSilverlightLoad" />
        <param name="enableGPUAcceleration" value="true" />
        <param name="background" value="white" />
        <param name="minRuntimeVersion" value="5.0.61118.0" />
        <param name="autoUpgrade" value="true" />
        <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration:none"> <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style:none"/> </a>
      </object>
    <iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
  </form>

</body>

</html>
