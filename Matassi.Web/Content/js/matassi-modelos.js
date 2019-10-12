var topeOriginal = 0;

$(document).ready(function () {

	$("#ulListaTiposModelos li").click(function () {
		$([document.documentElement, document.body]).animate({
			scrollTop: $("#" + $(this).data('divmodelos')).offset().top - 65
		}, 1000);
	});

	topeOriginal = $("#divBannerModelos").offset().top;

});

function Utils() {

}

Utils.prototype = {
	constructor: Utils,
	isElementInView: function (element, fullyInView, margenTop, restarAltura) {
		var pageTop = $(window).scrollTop() + margenTop;
		var pageBottom = pageTop + $(window).height();
		var elementTop = $(element).offset().top;
		var elementBottom = elementTop + $(element).height() - restarAltura;

		if (fullyInView === true) {
			return ((pageTop < elementTop) && (pageBottom > elementBottom));
		} else {
			return ((elementTop <= pageBottom) && (elementBottom >= pageTop));
		}
	}
};

var Utils = new Utils();

$(document).scroll(function () {
	$("#ulListaTiposModelos li").removeClass('seleccionado');

	if (Utils.isElementInView($('#divCompactos'), false, 65, 50))
		$("#ulListaTiposModelos li.liCompactos").addClass('seleccionado');
	else if (Utils.isElementInView($('#divSedanes'), false, 65, 50))
		$("#ulListaTiposModelos li.liSedanes").addClass('seleccionado');
	else if (Utils.isElementInView($('#divFamiliares'), false, 65, 50))
		$("#ulListaTiposModelos li.liFamiliares").addClass('seleccionado');
	else if (Utils.isElementInView($('#divSuv'), false, 65, 50))
		$("#ulListaTiposModelos li.liSuv").addClass('seleccionado');
	else if (Utils.isElementInView($('#divSmallPickUp'), false, 65, 50))
		$("#ulListaTiposModelos li.liSmallPickUp").addClass('seleccionado');
	else if (Utils.isElementInView($('#divComerciales'), false, 65, 50))
		$("#ulListaTiposModelos li.liComerciales").addClass('seleccionado');

	var tope = topeOriginal - $(window).scrollTop();

	if (tope >= 0)
	{
		tope += 'px';
		$("#divBannerModelos").css({'top': tope});
	}
	else{
		$("#divBannerModelos").css({ 'top': '0px' });
	}

});