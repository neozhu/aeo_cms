//by HK-5138 9-14-2012
(function($) {
    //    $.fn.IsHide = function(option) {
    //        var isHide = $(this).is(":hidden");
    //        if (option === true && $(this).is(":hidden")) {
    //            isHide = true;
    //            $(this).show();
    //            $("#StartAddress").val();
    //            $("#EndAddress").val();
    //        }
    //        else if (option === false && !$(this).is(":hidden")) {
    //            isHide = false;
    //            $(this).hide();
    //        }
    //        return isHide;
    //    }
    //    $.fn.SelectedText = function(option) {
    //        return $(this).find("option:selected").text();
    //    }
    //    $.fn.SetSelectedText = function(option) {
    //        $(this).find("option[text='" + option + "']").attr("selected", true);
    //    }

    function rePos(o) {
        var $x = $y = 0;
        do {
            $x += o.offsetLeft;
            $y += o.offsetTop;
        } while ((o = o.offsetParent)); // && o.tagName != "BODY"
        return { x: $x, y: $y };
    };

    $.fn.Disabled = function(title) {
        var div, pos;
        this.each(function() {
            //if (!o) return;
            div = this.disabled_div = document.body.appendChild(document.createElement("div")), pos = rePos(this);
            with (div.style) {
                position = "absolute";
                backgroundColor = "#FFFFFF";
                width = this.offsetWidth + "px";
                height = this.offsetHeight + "px";
                left = pos.x + "px";
                top = pos.y + "px";
                opacity = "0";
                filter = "alpha(opacity:0)";
            }
            div.title = title || "";
        });
        return this;
    };

    $.fn.Enabled = function() {
        this.each(function() {
            if (this && this.disabled_div) document.body.removeChild(this.disabled_div);
        });
        return this;
    }
})(jQuery);