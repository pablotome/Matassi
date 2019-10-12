










var preguntasVentas = [{
	ID: 1,
	Pregunta: '¿Cuál es su nivel de satisfacción con Matassi e Imperiale S.A?',
	Tipo: 1
},
{
	ID: 2,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto a la actitud del vendedor que lo/la atendió?',
	Tipo: 1
},
{
	ID: 3,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto al conocimiento que ha demostrado el vendedor sobre el producto que ha comprado y la operación en general?',
	Tipo: 1
},
{
	ID: 4,
	Pregunta: '¿En nuestro concesionario se le ha ofrecido realizar una prueba de manejo de un vehículo VW?',
	Tipo: 1
},
{
	ID: 5,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto a la gestión administrativa en cuanto a cordialidad?',
	Tipo: 1
},
{
	ID: 6,
	Pregunta: '¿Cuál es su nivel de satisfacción respecto a la facilidad de comunicarse y realizar consultas administrativas?',
	Tipo: 1
},
{
	ID: 7,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto a la explicación de los trámites administrativos y sus tiempos?',
	Tipo: 1
},
{
	ID: 8,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto a la entrega de su 0km en cuanto a condiciones técnicas y la limpieza?',
	Tipo: 1
},
{
	ID: 9,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto a la explicación del funcionamiento del vehiculo, mantenimiento, garantía, etc?',
	Tipo: 1
},
{
	ID: 10,
	Pregunta: '¿Cuál es su nivel de satisfacción con respecto al cumplimiento de la fecha y hora acordada?',
	Tipo: 1
},
{
	ID: 11,
	Pregunta: '¿Le informaron quien será su contacto post venta?',
	Tipo: 2
},
{
	ID: 12,
	Pregunta: '¿El vendedor se ha contactado con usted luego de la entrega?',
	Tipo: 2
},
{
	ID: 13,
	Pregunta: '¿Volvería a comprar en nuestro concesionario?',
	Tipo: 2
},
{
	ID: 14,
	Pregunta: '¿Está interesado en colocar accesorios?',
	Tipo: 2
}];


$(document).ready(function () {
	var opcionesPregunta;

	for (preguntaVentas in preguntasVentas) {
		if (preguntasVentas[preguntaVentas].Tipo == 1)
			opcionesPregunta = $('<div><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="Sumamente satisfecho">Sumamente satisfecho</label><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="Muy satisfecho">Muy satisfecho</label><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="Satisfecho">Satisfecho</label><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="Poco Satisfecho">Poco Satisfecho</label><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="Nada Satisfecho">Nada Satisfecho</label></div>');
		else if (preguntasVentas[preguntaVentas].Tipo == 2)
			opcionesPregunta = $('<div><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="SI">SI</label><label><input id="Pregunta' + preguntasVentas[preguntaVentas].ID + '" name="Pregunta' + preguntasVentas[preguntaVentas].ID + '" type="radio" value="NO">NO</label>');

		var tblPregunta = $('<div class="Imagenes1Columna"><div class="infoSoloTextoColumna"><h5>' + preguntasVentas[preguntaVentas].Pregunta + '</h5><p>' + $(opcionesPregunta).html() + '</p></div></div>');
		$('#divListaPreguntas .Imagenes3Columnas:last').before(tblPregunta);
	}

	$("#btnContestarEncuesta").click(function () {
		for (preguntaVentas in preguntasVentas) {
			if ($('input[name=Pregunta' + preguntasVentas[preguntaVentas].ID + ']:checked').val() === undefined) {
				alert('Debe contestar la pregunta "' + preguntasVentas[preguntaVentas].Pregunta + '"');
				return;
			}
		}

		var encuesta = "";

		for (preguntaVentas in preguntasVentas) {
			encuesta += '{"TextoPregunta":"' + preguntasVentas[preguntaVentas].Pregunta + '", "TextoRespuesta":"' + $('input[name=Pregunta' + preguntasVentas[preguntaVentas].ID + ']:checked').val() + '"}, ';
		}

		encuesta = '{"Preguntas":[' + encuesta.substring(0, encuesta.length - 2) + '], "Nombre":"' + $('#txtNombre').val() + '", "Telefono":"' + $('#txtTelefono').val() + '", "EMail":"' + $('#txtEMail').val() + '"}';

		encuesta = JSON.parse(encuesta);


		/*$.getJSON("/API/Matassi/ResponderEncuestaVenta/" + grupo + "?ord=" + orden, function (result) {

			$("#divEmisiones").empty();

			for (var i = 0; i < result.length; i++) {

				var tblOferta = $('<table id="tblEmision"><tbody><tr class="Encabezado"><th	colspan="4">APELLIDO Y NOMBRE DEL CLIENTE: ' + result[i].Nombre + '</th></tr><tr><td><b>VENCIMIENTO:</b></td><td><b>' + result[i].Vencimiento + '</b></td><td colspan="2"></td></tr><tr><td>GRUPO:</td><td>' + result[i].Grupo + '</td><td>ALICUOTA:</td><td>$ ' + result[i].Alicuota + '</td></tr><tr><td>ORDEN:</td><td>' + result[i].Orden + '</td><td>CARGOS ADM.:</td><td>$ ' + result[i].CargosAdm + '</td></tr><tr><td>DESVIO:</td><td>' + result[i].Desvio + '</td><td>ACT.ALICUOTA:</td><td>$ ' + result[i].ActAlicuota + '</td></tr><tr><td>CUOTA NRO:</td><td>' + result[i].NroCuota + '</td><td>CARGOS ACT.ALIC.:</td><td>$ ' + result[i].CargosActAlic + '</td></tr><tr><td>SEG.BIEN:</td><td>$ ' + result[i].SegBien + '</td><td>SEG.VIDA:</td><td>$ ' + result[i].SegVida + '</td></tr><tr><td>CUOTAS PLAN:</td><td>' + result[i].CuotasPlan + '</td><td>MORA:</td><td>$ ' + result[i].Mora + '</td></tr><tr><td>MODELO PLAN:</td><td>' + result[i].Modelo + '</td><td>DEB/CRED:</td><td>$ ' + result[i].DebCred + '</td></tr><tr><td>BANCO:</td><td>' + result[i].Banco + '</td><td>INT.LIQUIDACION:</td><td>$ ' + result[i].IntLiq + '</td></tr><tr><td>SUCURSAL:</td><td>' + result[i].Sucursal + '</td><td>OTROS:</td><td>$ ' + result[i].Otros + '</td></tr><tr><td>CUENTA:</td><td>' + result[i].Cuenta + '</td><td><b>***** TOTAL:</b></td><td><b>$ ' + result[i].Total + '</b></td></tr></tbody></table>');
				$("#divEmisiones").append(tblOferta);
				$("#divEmisiones").append('<br/>');

			}
		});*/

		$.ajax({
			type: "POST",
			dataType: 'json',
			cache: false,
			data: JSON.stringify(encuesta),
			//data: { '': encuesta },
			url: "/API/Matassi/ResponderEncuestaVenta",
			contentType: "application/json",
			success: function (response) {
				alert('Gracias por contestar la encuesta!');
				window.open(window.location, '_self');
			},
			error: function (jqXHR, textStatus, errorThrown) {
				alert('Error - ' + errorThrown);
			}
		});

	});

});