$(document).ready(function () {

	$("#txtFecha").datepicker(
		$.datepicker.regional["es"]
	);

	var modelos = ["UP!", "Gol", "Fox", "Golf", "The Beetle", "Scirocco", "Nuevo Polo", "Golf Gti", "Voyage", "Polo", "Vento", "Passat", "Nuevo Virtus", "Suran", "Golf Variant", "Nuevo Tiguan Allspace", "Touareg", "Saveiro", "Amarok"];

	for (var i in modelos)
	{
		$('#ddlModelo').append($('<option>', { value: modelos[i], text: modelos[i] }));
	}


	$("#btnEnviarContacto").click(function () {
		if ($('#txtNombre').val() == "" || $('#txtTelefono').val() == "" || $('#txtEMail').val() == "" || $('#txtComentarios').val() == ""
				|| $('#ddlModelo').val() == "" || $('#txtFecha').val() == "" || $('#ddlHora').val() == "") {
			alert('Debe completar todos los campos');
			return;
		}

		var datosContacto = '{"Comentarios":"' + $('#txtComentarios').val() + '", "Nombre":"' + $('#txtNombre').val() + '", "Telefono":"' + $('#txtTelefono').val() + '", "EMail":"' + $('#txtEMail').val() + '", "Modelo": "' + $('#ddlModelo').val() + '", "Fecha": "' + $('#txtFecha').val() + '", "Hora": "' + $('#ddlHora').val() + '"}';

		datosContacto = JSON.parse(datosContacto.replace(/\r?\n|\r/g, ''));


		$.ajax({
			type: "POST",
			dataType: 'json',
			cache: false,
			data: JSON.stringify(datosContacto),
			//data: { '': encuesta },
			url: "/API/Matassi/EnviarPruebaManejo",
			contentType: "application/json",
			success: function (response) {
				alert('Gracias por su solicitud. La misma será respondida a la brevedad.');
				window.open(window.location, '_self');
			},
			error: function (jqXHR, textStatus, errorThrown) {
				alert('Error - ' + errorThrown);
			}
		});

	});

});