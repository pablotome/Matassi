$(document).ready(function () {

	$("#btnEnviarContacto").click(function () {
		if ($('#txtNombre').val() == "" || $('#txtTelefono').val() == "" || $('#txtEMail').val() == "" || $('#txtComentarios').val() == "") {
			alert('Debe completar todos los campos');
			return;
		}

		var datosContacto = '{"Comentarios":"' + $('#txtComentarios').val() + '", "Nombre":"' + $('#txtNombre').val() + '", "Telefono":"' + $('#txtTelefono').val() + '", "EMail":"' + $('#txtEMail').val() + '"}';

		datosContacto = JSON.parse(datosContacto.replace(/\r?\n|\r/g, ''));


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
			data: JSON.stringify(datosContacto),
			//data: { '': encuesta },
			url: "/API/Matassi/EnviarContacto",
			contentType: "application/json",
			success: function (response) {
				alert('Gracias por su consulta. La misma será respondida a la brevedad.');
				window.open(window.location, '_self');
			},
			error: function (jqXHR, textStatus, errorThrown) {
				alert('Error - ' + errorThrown);
			}
		});

	});

});