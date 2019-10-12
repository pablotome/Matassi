$(document).ready(function () {
	$('.dropdown').stop().hover(
        function () {
        	$(this).children('.sub-menu').slideDown(120);
        },
        function () {
        	$(this).children('.sub-menu').slideUp(0);
        }
    );

	$("nav ul li:has(ul)").stop().hover(
		function () {
			$(this)
				.css('-webkit-box-shadow', '0px -2px 5px #e6e6e6')
				.css('-moz-box-shadow', '0px -2px 5px #e6e6e6')
				.css('box-shadow', '0px -2px 5px #e6e6e6');
		},
		function () {
			$(this)
				.css('-webkit-box-shadow', 'none')
				.css('-moz-box-shadow', 'none')
				.css('box-shadow', 'none');
		}
	);


	/*$("#content-slider").lightSlider({
		loop: true,
		keyPress: true
	});*/

	

}); // end ready