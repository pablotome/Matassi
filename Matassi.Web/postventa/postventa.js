$(document).ready(function () {
	$("#btnEnviarContacto").click(function () {
		if ($('#txtNombre').val() == ""
			|| $('#txtTelefono').val() == ""
			|| $('#txtEMail').val() == ""
			|| $('#txtComentariostxtNombre').val() == "") {
			alert('Debe completar todos los campos del formulario');
			return;
		}

		if ($('#chkPoliticas').prop("checked") == false) {
			alert('Debe declarar que conoce las políticas de privacidad');
			return;
		}

		var datosContacto = '{"Comentarios":"' + $('#txtComentarios').val() + '", "Nombre":"' + $('#txtNombre').val() + '", "Telefono":"' + $('#txtTelefono').val() + '", "EMail":"' + $('#txtEMail').val() + '"}';

		datosContacto = JSON.parse(datosContacto.replace(/\r?\n|\r/g, ''));

		$.ajax({
			type: "POST",
			dataType: 'json',
			cache: false,
			data: JSON.stringify(datosContacto),
			//data: { '': encuesta },
			url: "/API/Matassi/Turno",
			contentType: "application/json",
			success: function (response) {
				alert('Gracias por solicitar el turno!');
				window.open(window.location, '_self');
			},
			error: function (jqXHR, textStatus, errorThrown) {
				alert('Error - ' + errorThrown);
			}
		});

	});

});