/*
    Gallery Grid Main Script
    (c) 2013 bdwm.be
    For any questions please email me at jules@bdwm.be
    Version: 1.3
*/

var mobile =  /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent);
var scale = parseFloat(rgg_params[0].scale) || 1.0;
var intime = parseFloat(rgg_params[0].intime) || 100;
var outtime = parseFloat(rgg_params[0].outtime) || 100;

jQuery(document).ready(function(){
    if(!mobile) {
        
        if (jQuery().colorbox) {
            jQuery('.rgg_imagegrid a').colorbox();
        }
        
        // create copies from the images as placeholders to be used by imagegrid
        jQuery('.rgg_imagegrid img').each(function() {
            var $this = jQuery(this);
            var offset = $this.position();
            $this.clone().addClass('bbg_placeholder').css({ visibility : 'hidden' }).appendTo($this.parent());
            $this.css({ left:offset.left, top:offset.top, position: 'absolute', 'max-width' : 'none' }).addClass('bbg_img');
        });
    
        jQuery('.rgg_imagegrid').each(function() {
            
            $this = jQuery(this);
            var id = parseInt($this.attr('rgg_id'));
            var p = rgg_params[id-1];
            p.scale = parseFloat(p.scale) || 1.0;
            
            jQuery('.bbg_img', this).each(function() {
               // TODO: save all calculated values in array, each time the window is resized.
            });
            
            jQuery('.bbg_img', this).mouseenter(function() {
                var $clone = jQuery(this);
                var $placeholder = $clone.siblings('.bbg_placeholder').eq(0);
                
                $clone.unbind('mouseleave');
                
                var width = $placeholder.width();
                var height = $placeholder.height();
                var offset = $placeholder.position();
                
                var newwidth = width*p.scale;
                var newheight = height*p.scale;
                var newoffset = { top: offset.top - (newheight-height)/2, left: offset.left - (newwidth-width)/2 };
                var zindex = $clone.css('z-index');
                var newzindex = 999;
                
                var prev_box_shadow = $clone.css('box-shadow');
                $clone.css({'z-index': newzindex, 'box-shadow' : '0px 0px 4px #000'});
                $clone.animate({ left: newoffset.left, top: newoffset.top, height: newheight, width: newwidth },p.intime);
                
                
                $clone.mouseleave(function() {  
                    $clone.css({'z-index': zindex, 'box-shadow' : prev_box_shadow});         
                    $clone.animate({left: offset.left, top: offset.top, height: height, width: width, 'z-index' : zindex},p.outtime);
                    $clone.unbind('mouseleave');
                });
            });
        });
        
    } else {
        
        // if mobile
        jQuery('.rgg_imagegrid img').addClass('bbg_placeholder');
    }
    
    jQuery('.rgg_imagegrid').each(function() {
        $this = jQuery(this);
        var id = parseInt($this.attr('rgg_id'));
        var p = rgg_params[id-1];
        
        $this.gallerygrid({
            'maxrowheight' :  parseInt(p.maxrowheight) || 20,
            'margin' :        parseInt(p.margin) || 0,
            'items' : '.bbg_placeholder',
            'after_init' : function() { },
            'before' : function() { },
            'after' : function() {
                if (!mobile) {
                    jQuery('.bbg_placeholder').each(function() {
                        // update position of the absolute clones based on their sibling placeholder
                        $placeholder = jQuery(this);
                        $clone = $placeholder.siblings('.bbg_img').eq(0);
                        var offset = $placeholder.position();
                        
                        $clone.css({ left:offset.left, top:offset.top, position: 'absolute', width: $placeholder.width(), height: $placeholder.height() }).addClass('bbg_image');
                    });
                }
            }
        });
    });    
});