//Execute the callback in the plugin: https://stackoverflow.com/a/2534466
//Calling a function inside a jQuery plugin from outside: https://stackoverflow.com/questions/18185956/calling-a-function-inside-a-jquery-plugin-from-outside

"use strict";

(function ($) {

	$.fn.carouselMatassi = function (options, callback) {

		var self = this;
		var ulTitulos = $('ul:nth-child(1)', self);
		var ulImagenes = $('ul:nth-child(2)', self);
		var ulDescripciones = $('ul:nth-child(3)', self);

		$.fn.carouselMatassi.defaults = {
			accion: 'crear',
			espacioEntreImagenes: 0.1,
			cantidadImagenesMostrar: 2,
			nuevoItem: 0,
			mostrarIndice: false,
			mostrarBotones: false,
			ulTitulos: $('ul:eq(0)', self),
			ulImagenes: $('ul:eq(1)', self),
			ulIndice: $('ul:eq(2)', self),
			ulDescripciones: $('ul:eq(3)', self)
		};

		var opts = $.extend({}, $.fn.carouselMatassi.defaults, options);

		self.actualizando = false;

		var ActualizarTitulo = function (mensaje) {
			var $itemActual = $('li:nth-child(2)', opts.ulImagenes).data('pos');
			
			$('li', opts.ulTitulos).each(function (i, o) {
				if ($itemActual == i + 1) {
					$(o).addClass('linkCarouselEncendido');
					$(o).removeClass('normal');
				}
				else {
					$(o).removeClass('linkCarouselEncendido');
					$(o).addClass('normal');
				}
			});

			$('li', opts.ulDescripciones).each(function (i, o) {
				if ($(this).data('pos') == $itemActual) {
					$(o).removeClass('oculto');
					$(o).addClass('visible');
				}
				else {
					$(o).addClass('oculto');
					$(o).removeClass('visible');
				}
			});

			$('li a', opts.ulIndice).each(function (i, o) {
				if ($itemActual == i + 1) {
					$(o).addClass('active');
				}
				else {
					$(o).removeClass('active');
				}
			});

			$('li img', opts.ulImagenes).each(function (i, o) {
				$(o).css('opacity', '0.6');
			});

			$('li:nth-child(2) img', opts.ulImagenes).css('opacity', '1');

		}

		return self.each(function () {

			if (opts.accion == 'crear') {

				if (self.actualizando)
					return;

				self.actualizando = true;

				$(opts.ulImagenes).addClass('ulImagenes');

				self.data('cantImagenes', $('li', opts.ulImagenes).length);

				self.data('anchoContenedor', $(this).width());	//ancho del DIV

				//la imagen mide de ancho la (cantidadImagenesMostrar) del contenedor
				//menos el porcentaje que usa para separar (espacioEntreImagenes)
				self.data('longitudImagen',
							self.data('anchoContenedor') / opts.cantidadImagenesMostrar
							-
							self.data('anchoContenedor') / opts.cantidadImagenesMostrar * opts.espacioEntreImagenes);

				self.data('margen', self.data('anchoContenedor') / opts.cantidadImagenesMostrar * opts.espacioEntreImagenes / 2);

				self.data('anchoTotalImagen', self.data('longitudImagen') + self.data('margen') * 2);

				$("li", opts.ulImagenes).each(function (index) {
					$(this).width(self.data('longitudImagen'));
					$(this).css('margin-left', self.data('margen') + 'px');
					$(this).css('margin-right', self.data('margen') + 'px');
					$(this).data('pos', index + 1);
				});


				$("li", opts.ulImagenes).touchwipe({
					wipeLeft: function () { self.carouselMatassi($.extend(opts, { accion: 'verDerecha' })); },
					wipeRight: function () { self.carouselMatassi($.extend(opts, { accion: 'verIzquierda' })); },
					wipeUp: function () { /*alert("up");*/ },
					wipeDown: function () { /*alert("down");*/ },
					min_move_x: 20,
					min_move_y: 20,
					preventDefaultEvents: true
				});

				self.data('marginLeftActual', -1 * self.data('margen') + -1 * self.data('longitudImagen') / 2);
				$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));

				if (opts.mostrarIndice) {
					//self.after('<div class="divPaginador"><ul class="ulIndice"></ul></div>');

					$(opts.ulIndice).empty();

					$("li", opts.ulImagenes).each(function () {
						var $posicion = $(this).data('pos');
						//var $item = $('<li><a href="#">&nbsp;' + $(this).data('pos') + '&nbsp;</a></li>').click(function () { self.carouselMatassi($.extend(opts, { accion: 'irA', nuevoItem: $posicion })); });
						var $item = $('<li><a href="#">&nbsp;&nbsp;</a></li>').click(function () { self.carouselMatassi($.extend(opts, { accion: 'irA', nuevoItem: $posicion })); });
						$(opts.ulIndice).append($item);
					});
				}

				$('.listaHorizontalImagenes ul li', self).each(function () {
					var $posicion = $(this).data('pos');
					$(this).click(function () { self.carouselMatassi($.extend(opts, { accion: 'irA', nuevoItem: $posicion })); });
				});

				if (opts.mostrarBotones) {
					$('.divFlechaIzquierda', self).click(function () { self.carouselMatassi($.extend(opts, { accion: 'verIzquierda' })); });
					$('.divFlechaDerecha', self).click(function () { self.carouselMatassi($.extend(opts, { accion: 'verDerecha' })); });
				}
				else {
					$('.divFlechaIzquierda', self).addClass('oculto');
					$('.divFlechaDerecha', self).addClass('oculto');
				}
				
				self.addClass('divCarousel');
				self.data('itemActual', 2);

				self.carouselMatassi($.extend(opts, { accion: 'verIzquierda' }));

				self.actualizando = false;
			}
			else if (opts.accion == 'verDerecha') {

				if (self.actualizando)
					return;

				self.actualizando = true;

				//$('.botonDerecha, .botonIzquierda', self).addClass('oculto');

				self.data('marginLeftActual', self.data('marginLeftActual') - self.data('anchoTotalImagen'));

				$(opts.ulImagenes).animate({ 'margin-left': self.data('marginLeftActual') }, 500, function () {
					$('li', opts.ulImagenes).first('li').appendTo(opts.ulImagenes);
					self.data('marginLeftActual', self.data('marginLeftActual') + self.data('anchoTotalImagen'));
					$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));

					var itemActual = self.data('itemActual');

					itemActual += 1;

					if (itemActual > $('li', opts.ulImagenes).length)
						itemActual = 1;

					self.data('itemActual', itemActual);

					//$('.botonDerecha, .botonIzquierda', self).removeClass('oculto');

					self.actualizando = false;

					ActualizarTitulo('derecha');
				});
			}
			else if (opts.accion == 'verIzquierda') {

				if (self.actualizando)
					return;

				self.actualizando = true;
				//$('.botonDerecha, .botonIzquierda', self).addClass('oculto');

				$('li', opts.ulImagenes).last('li').insertBefore($('li', opts.ulImagenes).first('li'));
				self.data('marginLeftActual', self.data('marginLeftActual') - self.data('anchoTotalImagen'));
				$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));

				self.data('marginLeftActual', self.data('marginLeftActual') + self.data('anchoTotalImagen'));

				$(opts.ulImagenes).animate({ 'margin-left': self.data('marginLeftActual') }, 500, function () {

					$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));

					var itemActual = self.data('itemActual');

					itemActual -= 1;

					if (itemActual < 1)
						itemActual = $('li', opts.ulImagenes).length;

					self.data('itemActual', itemActual);

					//$('.botonDerecha, .botonIzquierda', self).removeClass('oculto');

					self.actualizando = false;

					ActualizarTitulo('izquierda');

				});
			}
			else if (opts.accion == 'irA') {

				var itemActual = self.data('itemActual');

				if (self.actualizando || itemActual == opts.nuevoItem || opts.nuevoItem > self.data('cantImagenes'))
					return;

				self.actualizando = true;

				var cantidadSaltos = Math.abs(opts.nuevoItem - itemActual);

				if (itemActual < opts.nuevoItem) {
					self.data('marginLeftActual', self.data('marginLeftActual') - self.data('anchoTotalImagen') * (opts.nuevoItem - itemActual));
					$(opts.ulImagenes).animate({ 'margin-left': self.data('marginLeftActual') }, 500, function () {

						do {
							$('li', opts.ulImagenes).first('li').appendTo(opts.ulImagenes);
							self.data('marginLeftActual', self.data('marginLeftActual') + self.data('anchoTotalImagen'));
							$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));
						} while (++itemActual < opts.nuevoItem)

						self.data('itemActual', itemActual);

						self.actualizando = false;

						ActualizarTitulo('ir a');

					});
				}
				else if (opts.nuevoItem < itemActual) {

					self.data('marginLeftActual', self.data('marginLeftActual') + self.data('anchoTotalImagen') * (itemActual - opts.nuevoItem));

					$(opts.ulImagenes).animate({ 'margin-left': self.data('marginLeftActual') }, 500, function () {

						do {
							$('li', opts.ulImagenes).last('li').insertBefore($('li', opts.ulImagenes).first('li'));
							self.data('marginLeftActual', self.data('marginLeftActual') - self.data('anchoTotalImagen'));
							$(opts.ulImagenes).css('margin-left', self.data('marginLeftActual'));
						} while (--itemActual > opts.nuevoItem)

						self.data('itemActual', itemActual);

						self.actualizando = false;

						ActualizarTitulo('ir a');

					});
				}
			}


			if (typeof callback == 'function') { // make sure the callback is a function
				callback.call(this); // brings the scope to the callback
			}
		});

	};

	$(window).resize(function () {
		//$(this).css('margin-top', $marginTop + 'px');
		//this.carouselMatassi();
	});

}(jQuery));