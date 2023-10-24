/*https://github.com/skidding/dragdealer*/
    (function(root,factory){if(typeof define==="function"&&define.amd){define(factory)}else{root.Dragdealer=factory()}})(this,function(){var Dragdealer=function(wrapper,options){this.bindMethods();this.options=this.applyDefaults(options||{});this.wrapper=this.getWrapperElement(wrapper);if(!this.wrapper){return}this.handle=this.getHandleElement(this.wrapper,this.options.handleClass);if(!this.handle){return}this.init();this.bindEventListeners()};Dragdealer.prototype={defaults:{disabled:false,horizontal:true,vertical:false,slide:true,steps:0,snap:false,loose:false,speed:.1,xPrecision:0,yPrecision:0,handleClass:"handle"},init:function(){this.value={prev:[-1,-1],current:[this.options.x||0,this.options.y||0],target:[this.options.x||0,this.options.y||0]};this.offset={wrapper:[0,0],mouse:[0,0],prev:[-999999,-999999],current:[0,0],target:[0,0]};this.change=[0,0];this.stepRatios=this.calculateStepRatios();this.activity=false;this.dragging=false;this.tapping=false;this.reflow();if(this.options.disabled){this.disable()}},applyDefaults:function(options){for(var k in this.defaults){if(!options.hasOwnProperty(k)){options[k]=this.defaults[k]}}return options},getWrapperElement:function(wrapper){if(typeof wrapper=="string"){return document.getElementById(wrapper)}else{return wrapper}},getHandleElement:function(wrapper,handleClass){var childElements=wrapper.getElementsByTagName("div"),handleClassMatcher=new RegExp("(^|\\s)"+handleClass+"(\\s|$)"),i;for(i=0;i<childElements.length;i++){if(handleClassMatcher.test(childElements[i].className)){return childElements[i]}}},calculateStepRatios:function(){var stepRatios=[];if(this.options.steps>1){for(var i=0;i<=this.options.steps-1;i++){stepRatios[i]=i/(this.options.steps-1)}}return stepRatios},setWrapperOffset:function(){this.offset.wrapper=Position.get(this.wrapper)},calculateBounds:function(){var bounds={top:this.options.top||0,bottom:-(this.options.bottom||0)+this.wrapper.offsetHeight,left:this.options.left||0,right:-(this.options.right||0)+this.wrapper.offsetWidth};bounds.availWidth=bounds.right-bounds.left-this.handle.offsetWidth;bounds.availHeight=bounds.bottom-bounds.top-this.handle.offsetHeight;return bounds},calculateValuePrecision:function(){var xPrecision=this.options.xPrecision||Math.abs(this.bounds.availWidth),yPrecision=this.options.yPrecision||Math.abs(this.bounds.availHeight);return[xPrecision?1/xPrecision:0,yPrecision?1/yPrecision:0]},bindMethods:function(){this.onHandleMouseDown=bind(this.onHandleMouseDown,this);this.onHandleTouchStart=bind(this.onHandleTouchStart,this);this.onDocumentMouseMove=bind(this.onDocumentMouseMove,this);this.onWrapperTouchMove=bind(this.onWrapperTouchMove,this);this.onWrapperMouseDown=bind(this.onWrapperMouseDown,this);this.onWrapperTouchStart=bind(this.onWrapperTouchStart,this);this.onDocumentMouseUp=bind(this.onDocumentMouseUp,this);this.onDocumentTouchEnd=bind(this.onDocumentTouchEnd,this);this.onHandleClick=bind(this.onHandleClick,this);this.onWindowResize=bind(this.onWindowResize,this)},bindEventListeners:function(){addEventListener(this.handle,"mousedown",this.onHandleMouseDown);addEventListener(this.handle,"touchstart",this.onHandleTouchStart);addEventListener(document,"mousemove",this.onDocumentMouseMove);addEventListener(this.wrapper,"touchmove",this.onWrapperTouchMove);addEventListener(this.wrapper,"mousedown",this.onWrapperMouseDown);addEventListener(this.wrapper,"touchstart",this.onWrapperTouchStart);addEventListener(document,"mouseup",this.onDocumentMouseUp);addEventListener(document,"touchend",this.onDocumentTouchEnd);addEventListener(this.handle,"click",this.onHandleClick);addEventListener(window,"resize",this.onWindowResize);var _this=this;this.interval=setInterval(function(){_this.animate()},25);this.animate(false,true)},unbindEventListeners:function(){removeEventListener(this.handle,"mousedown",this.onHandleMouseDown);removeEventListener(this.handle,"touchstart",this.onHandleTouchStart);removeEventListener(document,"mousemove",this.onDocumentMouseMove);removeEventListener(this.wrapper,"touchmove",this.onWrapperTouchMove);removeEventListener(this.wrapper,"mousedown",this.onWrapperMouseDown);removeEventListener(this.wrapper,"touchstart",this.onWrapperTouchStart);removeEventListener(document,"mouseup",this.onDocumentMouseUp);removeEventListener(document,"touchend",this.onDocumentTouchEnd);removeEventListener(this.handle,"click",this.onHandleClick);removeEventListener(window,"resize",this.onWindowResize);clearInterval(this.interval)},onHandleMouseDown:function(e){Cursor.refresh(e);preventEventDefaults(e);stopEventPropagation(e);this.activity=false;this.startDrag()},onHandleTouchStart:function(e){Cursor.refresh(e);stopEventPropagation(e);this.activity=false;this.startDrag()},onDocumentMouseMove:function(e){Cursor.refresh(e);if(this.dragging){this.activity=true}},onWrapperTouchMove:function(e){Cursor.refresh(e);if(!this.activity&&this.draggingOnDisabledAxis()){if(this.dragging){this.stopDrag()}return}preventEventDefaults(e);this.activity=true},onWrapperMouseDown:function(e){Cursor.refresh(e);preventEventDefaults(e);this.startTap()},onWrapperTouchStart:function(e){Cursor.refresh(e);preventEventDefaults(e);this.startTap()},onDocumentMouseUp:function(e){this.stopDrag();this.stopTap()},onDocumentTouchEnd:function(e){this.stopDrag();this.stopTap()},onHandleClick:function(e){if(this.activity){preventEventDefaults(e);stopEventPropagation(e)}},onWindowResize:function(e){this.reflow()},enable:function(){this.disabled=false;this.handle.className=this.handle.className.replace(/\s?disabled/g,"")},disable:function(){this.disabled=true;this.handle.className+=" disabled"},reflow:function(){this.setWrapperOffset();this.bounds=this.calculateBounds();this.valuePrecision=this.calculateValuePrecision();this.updateOffsetFromValue()},getStep:function(){return[this.getStepNumber(this.value.target[0]),this.getStepNumber(this.value.target[1])]},getValue:function(){return this.value.target},setStep:function(x,y,snap){this.setValue(this.options.steps&&x>1?(x-1)/(this.options.steps-1):0,this.options.steps&&y>1?(y-1)/(this.options.steps-1):0,snap)},setValue:function(x,y,snap){this.setTargetValue([x,y||0]);if(snap){this.groupCopy(this.value.current,this.value.target);this.updateOffsetFromValue();this.callAnimationCallback()}},startTap:function(){if(this.disabled){return}this.tapping=true;this.setWrapperOffset();this.setTargetValueByOffset([Cursor.x-this.offset.wrapper[0]-this.handle.offsetWidth/2,Cursor.y-this.offset.wrapper[1]-this.handle.offsetHeight/2])},stopTap:function(){if(this.disabled||!this.tapping){return}this.tapping=false;this.setTargetValue(this.value.current)},startDrag:function(){if(this.disabled){return}this.dragging=true;this.setWrapperOffset();this.offset.mouse=[Cursor.x-Position.get(this.handle)[0],Cursor.y-Position.get(this.handle)[1]]},stopDrag:function(){if(this.disabled||!this.dragging){return}this.dragging=false;var target=this.groupClone(this.value.current);if(this.options.slide){var ratioChange=this.change;target[0]+=ratioChange[0]*4;target[1]+=ratioChange[1]*4}this.setTargetValue(target)},callAnimationCallback:function(){var value=this.value.current;if(this.options.snap&&this.options.steps>1){value=this.getClosestSteps(value)}if(!this.groupCompare(value,this.value.prev)){if(typeof this.options.animationCallback=="function"){this.options.animationCallback.call(this,value[0],value[1])}this.groupCopy(this.value.prev,value)}},callTargetCallback:function(){if(typeof this.options.callback=="function"){this.options.callback.call(this,this.value.target[0],this.value.target[1])}},animate:function(direct,first){if(direct&&!this.dragging){return}if(this.dragging){var prevTarget=this.groupClone(this.value.target);var offset=[Cursor.x-this.offset.wrapper[0]-this.offset.mouse[0],Cursor.y-this.offset.wrapper[1]-this.offset.mouse[1]];this.setTargetValueByOffset(offset,this.options.loose);this.change=[this.value.target[0]-prevTarget[0],this.value.target[1]-prevTarget[1]]}if(this.dragging||first){this.groupCopy(this.value.current,this.value.target)}if(this.dragging||this.glide()||first){this.updateOffsetFromValue();this.callAnimationCallback()}},glide:function(){var diff=[this.value.target[0]-this.value.current[0],this.value.target[1]-this.value.current[1]];if(!diff[0]&&!diff[1]){return false}if(Math.abs(diff[0])>this.valuePrecision[0]||Math.abs(diff[1])>this.valuePrecision[1]){this.value.current[0]+=diff[0]*this.options.speed;this.value.current[1]+=diff[1]*this.options.speed}else{this.groupCopy(this.value.current,this.value.target)}return true},updateOffsetFromValue:function(){if(!this.options.snap){this.offset.current=this.getOffsetsByRatios(this.value.current)}else{this.offset.current=this.getOffsetsByRatios(this.getClosestSteps(this.value.current))}if(!this.groupCompare(this.offset.current,this.offset.prev)){this.renderHandlePosition();this.groupCopy(this.offset.prev,this.offset.current)}},renderHandlePosition:function(){if(this.options.horizontal){this.handle.style.left=String(this.offset.current[0])+"px"}if(this.options.vertical){this.handle.style.top=String(this.offset.current[1])+"px"}},setTargetValue:function(value,loose){var target=loose?this.getLooseValue(value):this.getProperValue(value);this.groupCopy(this.value.target,target);this.offset.target=this.getOffsetsByRatios(target);this.callTargetCallback()},setTargetValueByOffset:function(offset,loose){var value=this.getRatiosByOffsets(offset);var target=loose?this.getLooseValue(value):this.getProperValue(value);this.groupCopy(this.value.target,target);this.offset.target=this.getOffsetsByRatios(target)},getLooseValue:function(value){var proper=this.getProperValue(value);return[proper[0]+(value[0]-proper[0])/4,proper[1]+(value[1]-proper[1])/4]},getProperValue:function(value){var proper=this.groupClone(value);proper[0]=Math.max(proper[0],0);proper[1]=Math.max(proper[1],0);proper[0]=Math.min(proper[0],1);proper[1]=Math.min(proper[1],1);if(!this.dragging&&!this.tapping||this.options.snap){if(this.options.steps>1){proper=this.getClosestSteps(proper)}}return proper},getRatiosByOffsets:function(group){return[this.getRatioByOffset(group[0],this.bounds.availWidth,this.bounds.left),this.getRatioByOffset(group[1],this.bounds.availHeight,this.bounds.top)]},getRatioByOffset:function(offset,range,padding){return range?(offset-padding)/range:0},getOffsetsByRatios:function(group){return[this.getOffsetByRatio(group[0],this.bounds.availWidth,this.bounds.left),this.getOffsetByRatio(group[1],this.bounds.availHeight,this.bounds.top)]},getOffsetByRatio:function(ratio,range,padding){return Math.round(ratio*range)+padding},getStepNumber:function(value){return this.getClosestStep(value)*(this.options.steps-1)+1},getClosestSteps:function(group){return[this.getClosestStep(group[0]),this.getClosestStep(group[1])]},getClosestStep:function(value){var k=0;var min=1;for(var i=0;i<=this.options.steps-1;i++){if(Math.abs(this.stepRatios[i]-value)<min){min=Math.abs(this.stepRatios[i]-value);k=i}}return this.stepRatios[k]},groupCompare:function(a,b){return a[0]==b[0]&&a[1]==b[1]},groupCopy:function(a,b){a[0]=b[0];a[1]=b[1]},groupClone:function(a){return[a[0],a[1]]},draggingOnDisabledAxis:function(){return!this.options.horizontal&&Cursor.xDiff>Cursor.yDiff||!this.options.vertical&&Cursor.yDiff>Cursor.xDiff}};var bind=function(fn,context){return function(){return fn.apply(context,arguments)}};var addEventListener=function(element,type,callback){if(element.addEventListener){element.addEventListener(type,callback,false)}else if(element.attachEvent){element.attachEvent("on"+type,callback)}};var removeEventListener=function(element,type,callback){if(element.removeEventListener){element.removeEventListener(type,callback,false)}else if(element.detachEvent){element.detachEvent("on"+type,callback)}};var preventEventDefaults=function(e){if(!e){e=window.event}if(e.preventDefault){e.preventDefault()}e.returnValue=false};var stopEventPropagation=function(e){if(!e){e=window.event}if(e.stopPropagation){e.stopPropagation()}e.cancelBubble=true};var Cursor={x:0,y:0,xDiff:0,yDiff:0,refresh:function(e){if(!e){e=window.event}if(e.type=="mousemove"){this.set(e)}else if(e.touches){this.set(e.touches[0])}},set:function(e){var lastX=this.x,lastY=this.y;if(e.pageX||e.pageY){this.x=e.pageX;this.y=e.pageY}else if(e.clientX||e.clientY){this.x=e.clientX+document.body.scrollLeft+document.documentElement.scrollLeft;this.y=e.clientY+document.body.scrollTop+document.documentElement.scrollTop}this.xDiff=Math.abs(this.x-lastX);this.yDiff=Math.abs(this.y-lastY)}};var Position={get:function(obj){var curleft=0,curtop=0;if(obj.offsetParent){do{curleft+=obj.offsetLeft;curtop+=obj.offsetTop}while(obj=obj.offsetParent)}return[curleft,curtop]}};return Dragdealer});
    
    
$(document).ready(function(){
     priceAddmoreRangeSlider();    
});

function priceAddmoreRangeSlider(){
    $("#green-highlight").css("width", "0%");
    $("#orange-highlight").css("width", "0%");         
    $("#pay_for_added_more_users").attr("disabled","disabled");
    //Days left in month/year
    var plan_days_left = pln.plan_days_left;
    var days_in_curr_month = pln.days_in_curr_month;
    var plan_days_left_in_year = pln.plan_days_left_in_year;
    var plan_days_left_in_month = pln.plan_days_left_in_month;
    var days_in_curr_year = pln.days_in_curr_year;
    var month_number = pln.month_number;
    var plan_starting_on = pln.plan_starting_on;
    var plan_ending_on_month = pln.plan_ending_on_month;
    var plan_ending_on_year = pln.plan_ending_on_year;
    var plan_total_no_users = pln.plan_total_no_users;

    //set defaults
    var plan_type_value = pln.plan_type_value;
    var plan_price = pln.price_per_user; 
    //alert(plan_type_value);
    if( plan_type_value == 1 ){               
        $(".annual-label").html("MONTHLY");
    } else if( plan_type_value == 2 ){
        $(".annual-label").html("YEARLY")
    }
    $("#plan-holder").text('Plan:');
    $("#device-holder").text(pln.plan_title+' Users');
    $(".info-price").html(pln.plan_currency_symbol+' '+ plan_price +' per user/ month');
    $(".annual-price").html('$0');

    new Dragdealer('pr-slider', {
        steps: 204,
        x: 0,
        y: 0,
        handleClass: "handle",
        animationCallback: function(x, y) {
            //set defaults
            
            //alert(plan_type_value);
    
            var slider_value = ((Math.round(x * 1000)));
            if(slider_value < plan_total_no_users){
                slider_value = plan_total_no_users;
                //if(slider_value < 50){ $(".handle").css("left", "" + slider_value + "px"); }
                //if(slider_value > 50){ $(".handle").css("left", "" + slider_value-48 + "px"); }
                $(".handle").css("left", "" + (slider_value - ((slider_value*21)/100)) + "px");
            }
            //$("#pr-slider .value").text(slider_value);
            var added_more_users = slider_value - plan_total_no_users;
            $(".added_more_users_in_plan").html(added_more_users);
            $(".totalusers-number").text(slider_value);
            
            var stripe_width = slider_value + 1;
            //alert(stripe_width);
            if(stripe_width <= 10){ $(".stripe").css("width", "" + (stripe_width/10) + "%"); }
            if(stripe_width >= 20){ $(".stripe").css("width", "" + (stripe_width/10) + "%"); }
            if(stripe_width >= 30){ $(".stripe").css("width", "" + (stripe_width/7) + "%"); }
            if(stripe_width >= 50){ $(".stripe").css("width", "" + (stripe_width/7) + "%"); }
            if(stripe_width >= 100){ $(".stripe").css("width", "" + (stripe_width/8) + "%"); }
            if(stripe_width >= 200){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }

            if(stripe_width >= 300){ $(".stripe").css("width", ""+(stripe_width/9)+"%"); }

            if(stripe_width >= 350){ $(".stripe").css("width", ""+(stripe_width/9)+"%"); }

            if(stripe_width >= 400){ $(".stripe").css("width", ""+(stripe_width/9)+"%"); }
            
            if(stripe_width >= 450){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }

            if(stripe_width >= 500){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }
            
            if(stripe_width >= 600){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }
            
            if(stripe_width >= 700){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }

            if(stripe_width >= 800){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }

            if(stripe_width >= 900){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }

            if(stripe_width >= 950){ $(".stripe").css("width", ""+(stripe_width/10)+"%"); }
            //
            
            $("#plan-holder").text('Plan:');
            $("#device-holder").text(pln.plan_title+' Users');
            $(".info-price").html(pln.plan_currency_symbol+' '+ plan_price +' per user/ month');
            
            if( added_more_users > 0 ){
                var total_amount_value = priceCalculation(plan_type_value, plan_price, added_more_users);
                $(".annual-price").html(pln.plan_currency_symbol+' '+total_amount_value);                
                $(".added_more_total_in_plan").html(total_amount_value);
                $("#pay_for_added_more_users").attr("disabled",false);
                $("#green-highlight").hide();          
            } else {
                var total_amount_value = priceCalculation(plan_type_value, plan_price, added_more_users);
                $(".annual-price").html(pln.plan_currency_symbol+' '+total_amount_value);                
                $(".added_more_total_in_plan").html(total_amount_value);
                $("#green-highlight").hide();
                $("#pay_for_added_more_users").attr("disabled","disabled");
                //show_error_message('response_messages_add_more_users', "You can only add more users more than Total Paid Users for open month");
            }
        }
    });
}

function priceCalculation(plan_type_value, plan_price, number_of_users){
    
    //Days left in month/year
    var plan_days_left = pln.plan_days_left;
    var days_in_curr_month = pln.days_in_curr_month;
    var plan_days_left_in_year = pln.plan_days_left_in_year;
    var plan_days_left_in_month = pln.plan_days_left_in_month;
    var days_in_curr_year = pln.days_in_curr_year;
    var month_number = pln.month_number;
    var plan_discount = pln.plan_discount;
    
    if( plan_type_value ==  1 ) {
        if( plan_days_left < days_in_curr_month ) {
            var per_day_price = plan_price/days_in_curr_month;
            var total=isNaN(parseInt(number_of_users* plan_price)) ? 0 :(number_of_users*per_day_price*plan_days_left )
            total_amount = total;
        } else {
            var total=isNaN(parseInt(number_of_users* plan_price)) ? 0 :(number_of_users* plan_price)
            total_amount = total;
        }
    } else if ( plan_type_value ==  2 ) {
        if( plan_days_left < days_in_curr_year ) {
            var per_day_price = plan_price/days_in_curr_month;
            var total_in_month=isNaN(parseInt(number_of_users* plan_price)) ? 0 :(number_of_users*per_day_price*plan_days_left_in_month );

            var months_left_in_year = 12 - month_number;
            var total_in_next_months=isNaN(parseInt(number_of_users* plan_price)) ? 0 :(number_of_users*plan_price*months_left_in_year );

            total = total_in_month+total_in_next_months;
            total_amount = total_in_month+total_in_next_months;

            if( plan_discount > 0 ) {
                discount_amount = ((total*plan_discount)/100);
            }
            total = total - discount_amount;
        } else {
            var total=isNaN(parseInt(number_of_users* plan_price)) ? 0 :(number_of_users* plan_price*12);
            total_amount = total;

            if( plan_discount > 0 ) {
                discount_amount = ((total*plan_discount)/100);
            }
            total = total - discount_amount;
        }
    }
    
    total = total.toFixed(2);
    total_amount = total_amount.toFixed(2);
    //$("#total_price").html(total_amount);
    //$("#net_total_price").html(total);
    if( plan_type_value == 2 ) {
        //monthly = monthly.toFixed(2);
        //$("#monthly_price").html(monthly);
        //$("#discount_amount").html('-'+discount_amount);
    }
    return total;
}
