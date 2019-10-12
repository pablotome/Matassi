$(document).ready(function () {

	$('.divImagenesNivel1').slick({
		prevArrow: '<a class="paginador paginadorIzquierda" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaIzquierda.svg" class="svg paginadorImagen"/></a>',
		nextArrow: '<a class="paginador paginadorDerecha" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaDerecha.svg" class="svg paginadorImagen"/></a>'
	});

	$('.divImagenesNivel1').on('beforeChange', function (event, slick, currentSlide, nextSlide) {
		//console.log(nextSlide);
		var $url = $('div[data-slick-index="' + nextSlide + '"] div div').data('url');
		$("div.divBotonConocelo").click(function () {
			window.open($url, '_self');
		});
	});

	var $url = $('div[data-slick-index="0"] div div').data('url');
	$("div.divBotonConocelo").click(function () {
		window.open($url, '_self');
	});

	var cantidadModelos = 0;

	if ($(window).width() <= 426) {
		$('.divAccesosDirecto1').slick({
			prevArrow: '<a class="paginador paginadorIzquierda" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaIzquierda.svg" class="svg paginadorImagen"/></a>',
			nextArrow: '<a class="paginador paginadorDerecha" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaDerecha.svg" class="svg paginadorImagen"/></a>'
		});
		cantidadModelos = 2;
	}
	else if ($(window).width() <= 767) {
		$('.divAccesosDirecto1').slick({
			prevArrow: '<a class="paginador paginadorIzquierda" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaIzquierda.svg" class="svg paginadorImagen"/></a>',
			nextArrow: '<a class="paginador paginadorDerecha" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaDerecha.svg" class="svg paginadorImagen"/></a>'
		});
		cantidadModelos = 3;
	}
	else {
		cantidadModelos = 3;
	}

	$('.divListaModelos').slick({
		prevArrow: '<a class="paginador paginadorIzquierda" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaIzquierda.svg" class="svg paginadorImagen"/></a>',
		nextArrow: '<a class="paginador paginadorDerecha" href="#" data-jcarouselcontrol="true"><img border="0" src="Content/img/iconos/flechaDerecha.svg" class="svg paginadorImagen"/></a>',
		infinite: false,
		slidesToShow: cantidadModelos,
		slidesToScroll: cantidadModelos
	});

	$('.ui-loader').hide();

	$("#divTodosLosModelos").show();

	$(window).resize(function () {
		if ($(window).width() <= 767) {
			$('.divAccesosDirecto1').slick({ dots: true });
		}
	});

	CrearMenuPagina();

	InlineSVG();

	AgregarDivDireccion();

});

function InlineSVG() {
	/*
		* Replace all SVG images with inline SVG
		*/
	jQuery('img.svg').each(function () {
		var $img = jQuery(this);
		var imgID = $img.attr('id');
		var imgClass = $img.attr('class');
		var imgURL = $img.attr('src');

		jQuery.get(imgURL, function (data) {
			// Get the SVG tag, ignore the rest
			var $svg = jQuery(data).find('svg');

			// Add replaced image's ID to the new SVG
			if (typeof imgID !== 'undefined') {
				$svg = $svg.attr('id', imgID);
			}
			// Add replaced image's classes to the new SVG
			if (typeof imgClass !== 'undefined') {
				$svg = $svg.attr('class', imgClass + ' replaced-svg');
			}

			// Remove any invalid XML tags as per http://validator.w3.org
			$svg = $svg.removeAttr('xmlns:a');

			// Replace image with new SVG
			$img.replaceWith($svg);

		}, 'xml');

	});
}

function openNav() {
	$("#footer").hide();
	$("#divMenuCelular").css({ "left": "0%" });
	$("#content").css({ "overflow": "hidden", "position": "fixed" });
	$("#footer").css({ "position": "fixed" });
	$("#divMostrarMenuCelular").hide();
	$("#divOcultarMenuCelular").show();
}

function closeNav() {
	$("#divMenuCelular").css({ "left": "100%" });
	$("#content").css({ "overflow": "auto", "position": "" });
	$("#footer").css({ "position": "" });
	$("#divMostrarMenuCelular").show();
	$("#divOcultarMenuCelular").hide();
	$("#footer").show();
}

function EsCelular() {
	//return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
	return ($(window).width() <= 767);
}

var menuPagina = [{
	"Link": "/",
	"Icono": "LogoVWGrande.png",
	"Titulo": ""
	},
	{
		"Link": "/modelo/",
		"Icono": "icono-modelos.svg",
		"Titulo": "Modelos"
	},
	{
		"Link": "/empresa/inicio",
		"Icono": "icono-empresa.svg",
		"Titulo": "Empresa"
	},
	{
		"Link": "/empresa/Ventas_Corporativas",
		"Icono": "icono-ventas-corporativas.svg",
		"Titulo": "Ventas<br />Corporativas"
	},
	{
		"Link": "/postventa/garantia.html",
		"Icono": "icono-postventa.svg",
		"Titulo": "Postventa"
	},
	{
		"Link": "/autoahorro/Index.html",
		"Icono": "icono-autoahorro.svg",
		"Titulo": "Autoahorro"
	}/*,
	{
		"Link": "/pruebaManejo/index.html",
		"Icono": "icono-prueba-manejo.svg",
		"Titulo": "Prueba de<br />Manejo"
	}*/];

function CrearMenuPagina()
{
	if (!EsCelular()) {
		for (i in menuPagina) {
			if (i > 0)
				$("#menu_ul").append('<li class="liIcono"><div class="divIcono"><a href="' + menuPagina[i].Link + '" rel="external" class="aIcono"><div class="divImagenIcono"><img src="/Content/img/iconos/' + menuPagina[i].Icono + '" class="svg imgImagenIcono" /></div><span class="spnIcono">' + menuPagina[i].Titulo + '</span></a></div></li>');
			else
				$("#menu_ul").append('<li><div class="icono"><a href="' + menuPagina[i].Link + '" rel="external"><img src="/Content/img/' + menuPagina[i].Icono + '" alt="Matassi e Imperiale"></a></div></li>');
		}
	}
	else {
		for (i in menuPagina) {
			if (i > 0)
				$("#divMenuCelular ul").append('<li class="liIcono"><a href="' + menuPagina[i].Link + '" rel="external"><div class="divImagenIcono"><img src="/Content/img/iconos/' + menuPagina[i].Icono + '" class="svg iconoMenu" /></div><div class="divTextoIcono">' + menuPagina[i].Titulo.replace('<br />', ' ') + '</div></a></li>');
		}
	}
}

function AgregarDivDireccion()
{
	$('<div id="divBarraInfoDireccion">Matassi e Imperiale S.A. - Hipolito Yrigoyen 1603 - 2600 Venado Tuerto - Teléfono : (03462)-437500</div>').insertAfter('#divBarraSeparadora');
}