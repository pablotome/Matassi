"use strict";

var sticky;

$(document).ready(function () {

	$("#divGaleriaPopup-Imagenes").slick({
		prevArrow: '<a class="paginador paginadorIzquierda" href="#" data-jcarouselcontrol="true"><img border="0" src="../../Content/img/iconos/flechaIzquierda.svg" class="svg paginadorImagen"/></a>',
		nextArrow: '<a class="paginador paginadorDerecha" href="#" data-jcarouselcontrol="true"><img border="0" src="../../Content/img/iconos/flechaDerecha.svg" class="svg paginadorImagen"/></a>'
	});

	$(".divGaleria").click(function () {
		$('.md-modal').addClass('md-show');
		$("#divCerrarGaleria").show();
		$('.volverArriba').hide();
	});

	$("#divCerrarGaleria").click(function () {
		$(this).hide();
		$('.md-modal').removeClass('md-show');
		$('.volverArriba').show();
	});

	$(".div-items-informacion-tecnica h3").click(function () {
		$(this).next().toggle('slow');
	});

	$(".div-boton-info-tecnica").click(function () {
		$("#div-Info-Tecnica-" + $(this).data('modelo')).toggle("fast");

		if (EsCelular()) {
			$([document.documentElement, document.body]).animate({
				scrollTop: $("#div-Info-Tecnica-" + $(this).data('modelo')).offset().top - 20
			}, 1000);
		}
		else {
			$([document.documentElement, document.body]).animate({
				scrollTop: $("#div-Info-Tecnica-" + $(this).data('modelo')).offset().top - 120
			}, 1000);
		}

	});

	$(".div-items-informacion-tecnica-cerrar").click(function () {
		$(this).parent().toggle("fast");

		$([document.documentElement, document.body]).animate({
			scrollTop: $(this).parent().offset().top - 360
		}, 1000);
	});

	$('.div-items-informacion-tecnica').each(function (indice, objeto) {
		$(this).find('.div-item-informacion-tecnica').each(function (indice2, objeto2) {
			if (indice2 != 0)
				$(objeto2).hide();
		});
	});

	$("#ulListaCaracteristicasModelo li").click(function () {
		$([document.documentElement, document.body]).animate({
			scrollTop: $("#" + $(this).data('divmodelos')).offset().top - 65
		}, 1000);

		/*/$('#ulListaCaracteristicasModelo li').removeClass('seleccionado');
		$(this).addClass('seleccionado');*/
	});


	//Top actual del div de caracteristicas (para celular)
	//sticky = $('.CaracteristicasCelular').offsetTop;
	sticky = document.getElementById("divHeaderCaracteristicas").offsetTop;
	//alert(document.getElementById("myHeader").offsetTop)
	//alert(sticky);

	$('.iconoDesplegar').click(function () {
		ActivarMenuAccesosCaracteristicas();
	});

	if (EsCelular()) {
		$('.Imagenes2Columnas > div.iconoDesplegarSeccion, .Imagenes3Columnas > div.iconoDesplegarSeccion').click(function () {
			if ($('svg', this).hasClass('abajo'))
				$('svg', this).removeClass('abajo')
			else
				$('svg', this).addClass('abajo')

			$('.infoTextoColumna', $(this).parent()).parent().slideToggle('slow');
		});

		$('.Imagenes2Columnas > div.iconoDesplegarSeccion, .Imagenes3Columnas > div.iconoDesplegarSeccion').next().toggle();


		$('.div-version-modelo').each(function (indice, objeto) {
			if (indice != 0)
				$(objeto).hide();
		});

		$('.paginadorVersiones a').click(function () {
			var indicePaginador = $(this).attr('href').replace('#', '') - 1;
			$('.paginadorVersiones a').removeClass('active');
			$(this).addClass('active');
			$('.div-version-modelo').hide();
			$('.div-version-modelo').each(function (indice, objeto) {
				if (indice == indicePaginador)
					$(objeto).show();
			});
			$('.div-info-tecnica-modelo').hide()

			$([document.documentElement, document.body]).animate({
				scrollTop: $("#divVersiones").offset().top - 10
			}, 10);

		});

		$("#divHeaderCaracteristicas ul li").each(function (i, o) {
			$(o).click(function () {
				var divElemento = $(this);
				$('li', $(divElemento).parent()).removeClass('activo');
				$(divElemento).addClass('activo');
				ActivarMenuAccesosCaracteristicas();
				$([document.documentElement, document.body]).animate({
					//scrollTop: $("#elementtoScrollToID").offset().top
					//scrollTop: $(divElemento).offset().top
					scrollTop: $('#' + $(divElemento).data('divmodelos')).offset().top - 10
				}, 500);
			});
		});
	}

	$('.volverArriba').click(function () {
		$('.CaracteristicasCelular ul').hide('slow');
		$('html, body').animate({ scrollTop: 0 }, 'fast');
	});

	InlineSVG();

});

(function ($) {
	/**
	 * Copyright 2012, Digital Fusion
	 * Licensed under the MIT license.
	 * http://teamdf.com/jquery-plugins/license/
	 *
	 * @author Sam Sehnert
	 * @desc A small plugin that checks whether elements are within
	 *		 the user visible viewport of a web browser.
	 *		 only accounts for vertical position, not horizontal.
	 */
	$.fn.visible = function (partial, margenTop) {

		var $t = $(this),
	    	$w = $(window),
	    	viewTop = $w.scrollTop() + margenTop,
	    	viewBottom = viewTop + $w.height(),
	    	_top = $t.offset().top,
	    	_bottom = _top + $t.height(),
	    	compareTop = partial === true ? _bottom : _top,
	    	compareBottom = partial === true ? _top : _bottom;

		return ((compareBottom <= viewBottom) && (compareTop >= viewTop));
	};
})(jQuery);


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
	$("#ulListaCaracteristicasModelo li").removeClass('seleccionado');

	//var visible = $(this).visible(detectPartial);
	var margenTop = $(window).height() / 2;
	if ($('#divDestacados').length > 0 && $('#divDestacados').visible(true, margenTop))
		$(".liDestacados").addClass('seleccionado');
	else if ($('#divConectividad').length > 0 && $('#divConectividad').visible(true, margenTop))
		$(".liConectividad").addClass('seleccionado');
	else if ($('#divPerformance').length > 0 && $('#divPerformance').visible(true, margenTop))
		$(".liPerformance").addClass('seleccionado');
	else if ($('#divSeguridad').length > 0 && $('#divSeguridad').visible(true, margenTop))
		$(".liSeguridad").addClass('seleccionado');
	else if ($('#divDisenio').length > 0 && $('#divDisenio').visible(true, margenTop))
		$(".liDisenio").addClass('seleccionado');
	else if ($('#divTecnologia').length > 0 && $('#divTecnologia').visible(true, margenTop))
		$(".liTecnologia").addClass('seleccionado');
	else if ($('#divVersiones').length > 0 && $('#divVersiones').visible(true, margenTop))
		$(".liVersiones").addClass('seleccionado');
	else if ($('#divNovedades').length > 0 && $('#divNovedades').visible(true, margenTop))
		$(".liNovedades").addClass('seleccionado');
	else if ($('#divCaracteristicas').length > 0 && $('#divCaracteristicas').visible(true, margenTop))
		$(".liCaracteristicas").addClass('seleccionado');
	else if ($('#divContrato').length > 0 && $('#divContrato').visible(true, margenTop))
		$(".liContrato").addClass('seleccionado');
	else if ($('#divInnovacion').length > 0 && $('#divInnovacion').visible(true, margenTop))
		$(".liInnovacion").addClass('seleccionado');
	else if ($('#divConfort').length > 0 && $('#divConfort').visible(true, margenTop))
		$(".liConfort").addClass('seleccionado');
	else if ($('#divDeportividad').length > 0 && $('#divDeportividad').visible(true, margenTop))
		$(".liDeportividad").addClass('seleccionado');
	else if ($('#divEquipamiento').length > 0 && $('#divEquipamiento').visible(true, margenTop))
		$(".liEquipamiento").addClass('seleccionado');
	else if ($('#divEspacio').length > 0 && $('#divEspacio').visible(true, margenTop))
		$(".liEspacio").addClass('seleccionado');
	else if ($('#divPractical').length > 0 && $('#divPractical').visible(true, margenTop))
		$(".liPractical").addClass('seleccionado');
	else if ($('#divCool').length > 0 && $('#divCool').visible(true, margenTop))
		$(".liCool").addClass('seleccionado');
	else if ($('#divIntuitive').length > 0 && $('#divIntuitive').visible(true, margenTop))
		$(".liIntuitive").addClass('seleccionado');
	else if ($('#divSafe').length > 0 && $('#divSafe').visible(true, margenTop))
		$(".liSafe").addClass('seleccionado');



	
	
	
	







	if ($(window).scrollTop() > sticky) {
		$('.CaracteristicasCelular').addClass("sticky");
	} else {
		$('.CaracteristicasCelular').removeClass("sticky");
	}

	if ($(window).scrollTop() > 0 && EsCelular())
		$('.volverArriba').show();
	else
		$('.volverArriba').hide();

});


/*function MostrarInfoTecnica(divInfo)
{
	$("#"+divInfo).toggle("fast");
}*/

function ActivarMenuAccesosCaracteristicas() {
	if ($('.iconoDesplegar svg').hasClass('abajo')) {
		$('.iconoDesplegar svg').removeClass('abajo');
		$('.iconoDesplegar svg').addClass('arriba');
	}
	else {
		$('.iconoDesplegar svg').removeClass('arriba');
		$('.iconoDesplegar svg').addClass('abajo');
	}

	$('.CaracteristicasCelular ul').toggle('slow');
}