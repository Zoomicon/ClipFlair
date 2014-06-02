<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CropImage.ascx.cs" Inherits="MonoSoftware.MonoX.Controls.CropImage" %>

<script type="text/javascript">
    //<![CDATA[
    $(document).ready(function() {
        setTimeout(loadCrop, 50);        
    });

    function loadCrop() {  
        setupCrop();  
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (s, e) {
                setupCrop();
            });
        }
    };

    function setupCrop(){        
        $('.image-crop-holder').Jcrop({
            onSelect: storeCoords, aspectRatio:<%= this.ImageAspectRatio %>, trueSize: [<%= this.FileInfo.Width %>, <%= this.FileInfo.Height %>], 
            boxWidth: <%= this.BoxWidth %>, boxHeight : <%= this.BoxHeight %>, minSize: [<%= this.CropMinSizeWidth %>,<%= this.CropMinSizeHeight %>],
            setSelect: <%= !this.AllowSelectOnCrop ? 
            string.Format("[{0}, {1}, {2}, {3}]", this.GetUpScaledImageCenter(CropMinSizeWidth, true), this.GetUpScaledImageCenter(CropMinSizeHeight, false), 
            this.GetUpScaledImageCropArea(CropMinSizeWidth, true), this.GetUpScaledImageCropArea(CropMinSizeHeight, false)) : "null" %>, 
            allowSelect: <%= this.AllowSelectOnCrop.ToString().ToLower() %>, parentDiv: "#cropHolder", bgOpacity: <%= this.BackgroundOpacity %>
        });        
    };

    function storeCoords(c){
        jQuery('#<%= imgCropX.ClientID %>').val(parseInt(c.x));
	    jQuery('#<%= imgCropY.ClientID %>').val(parseInt(c.y));
        jQuery('#<%= imgCropH.ClientID %>').val(parseInt(c.h));
        jQuery('#<%= imgCropW.ClientID %>').val(parseInt(c.w));
    };    
     //]]>    
</script>

<div id="cropHolder" style="width:<%= this.WidthOnClient %>px; height:<%= this.HeightOnClient %>px;">    
    <asp:Image ID="imgCrop" runat="server" CssClass="image-crop-holder" />
    <asp:HiddenField ID="imgCropX" runat="server" EnableViewState="true"/>
    <asp:HiddenField ID="imgCropY" runat="server" EnableViewState="true"/>
    <asp:HiddenField ID="imgCropW" runat="server" EnableViewState="true"/>
    <asp:HiddenField ID="imgCropH" runat="server" EnableViewState="true"/>
</div>
<div>
    <div style="display: inline-block">
        <monox:StyledButton runat="server" Id="btnCrop" RegisterAsPostBackControl="true" />
    </div>
    <div style="display: inline-block">
        <monox:StyledButton runat="server" Id="btnCancel" RegisterAsPostBackControl="true" />    
    </div>
</div>