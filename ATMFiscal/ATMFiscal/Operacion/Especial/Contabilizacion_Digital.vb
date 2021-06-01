Imports Telerik.WinControls
Imports System
Imports System.IO
Imports CrystalDecisions.Shared
Imports ThoughtWorks.QRCode
Imports ThoughtWorks.QRCode.Codec
Imports System.Xml
Imports System.Collections
Imports System.Windows.Forms
Public Class Contabilizacion_Digital

	Private Class Factura
		Public Property ID As Integer
		Public Property Folio As String
		Public Property Serie As String
		Public Property Total As Decimal
		Public Property Sub_Total As Decimal
		Public Property Iva As Decimal
		Public Property Imp_Grabado As Decimal
		Public Property Imp_Exento As Decimal
		Public Property Total_Real As Decimal
		Public Property Banco_Origen As String
		Public Property Cuenta_Origen As String
		Public Property Banco_Destino As String
		Public Property Imp_Provision As Decimal
		Public Property Fecha_Emision As String
		Public Property Fecha_Timbrado As String
		Public Property RFC_Emisor As String
		Public Property Nombre_Emisor As String
		Public Property RFC_Receptor As String
		Public Property Nombre_Receptor As String


	End Class
	Private Function RegresaId(ByVal UUID As String, ByVal Campo As String, ByVal Tabla As String)
		Dim id As Integer = 0
		Dim ds As DataSet = Eventos.Obtener_DS("select " & Campo & " FROM " & Tabla & " WHERE ID_poliza IS NULL AND UUID ='" & UUID & "'")
		If ds.Tables(0).Rows.Count > 0 Then
			id = ds.Tables(0).Rows(0)(0)
		End If
		Return id
	End Function
	Private Function RegresaIdC(ByVal UUID As String, ByVal Campo As String, ByVal Tabla As String)
		Dim id As Integer = 0
		Dim ds As DataSet = Eventos.Obtener_DS("select " & Campo & " FROM " & Tabla & " WHERE ID_poliza IS NULL AND IdDocumento ='" & UUID & "'")
		If ds.Tables(0).Rows.Count > 0 Then
			id = ds.Tables(0).Rows(0)(0)
		End If
		Return id
	End Function
	Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Hacer.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaConceptos.SelectedIndex = 0 Then
			If Me.Tabla1.Rows.Count > 0 Then
				' Guardar_Carga()
				Dim Numero_Polzia As Integer
				Dim Id As Integer = 0
				If RadMessageBox.Show("Se contabilizara los Archivos digitales esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
					Me.Barra.Visible = True
					Me.Barra.Maximum = Me.Tabla1.RowCount - 1
					Me.Barra.Minimum = 0
					Me.Barra.Value1 = 0

					For p As Integer = 0 To Me.Tabla1.RowCount - 1


						Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (convert(INT,Num_Pol))+1 FROM Polizas WHERE Id_Empresa= 1 AND id_Anio = '" & Me.ComboAño.Text.Trim() & "' AND id_Mes ='" & Me.Tabla1.Item(Mes.Index, p).Value & "' and Id_Tipo_Pol_Sat = " & 13 & "")
						If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
							Numero_Polzia = 1
						Else
							Numero_Polzia = ds.Tables(0).Rows(0)(0)
						End If
						Id = RegresaId(Me.Tabla1.Item(UUID.Index, p).Value, "Id_Registro_Xml", "Xml_Sat")

                        Codificar_polizas(p, Me.Tabla1.Item(FechaTimbrado.Index, p).Value.ToString.Substring(6, 4), Me.Tabla1.Item(Mes.Index, p).Value.ToString, Me.Tabla1.Item(FechaTimbrado.Index, p).Value.ToString.Substring(0, 2), Me.Tabla1.Item(FechaTimbrado.Index, p).Value,
                                  "I P/Cobrar F/" & Me.Tabla1.Item(UUID.Index, p).Value, Me.Tabla1.Item(UUID.Index, p).Value, Numero_Polzia, Id, Me.Tabla1.Item(RFCReceptor.Index, p).Value,
                                 Me.Tabla1.Item(ImpProvision.Index, p).Value, Me.Tabla1.Item(NombreReceptor.Index, p).Value, Me.Tabla1.Item(Grabado.Index, p).Value, Me.Tabla1.Item(Exento.Index, p).Value, Me.Tabla1.Item(IvaR.Index, p).Value, 13, Me.Tabla1.Item(IVA6.Index, p).Value)




                        If Me.Barra.Value1 = Me.Barra.Maximum Then
							Me.Barra.Minimum = 0
							Me.Cursor = Cursors.Arrow
							RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
							Me.Barra.Value1 = 0
							Me.Barra.Visible = False
						Else
							Me.Barra.Value1 += 1
						End If
					Next
				End If

			Else
				RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			End If
		Else
			If Me.Tabla2.Rows.Count > 0 Then
				Dim Numero_Polzia As Integer
				Dim Id As Integer = 0
				If RadMessageBox.Show("Se contabilizara los Archivos digitales esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
					Me.Barra.Visible = True
					Me.Barra.Maximum = Me.Tabla2.RowCount - 1
					Me.Barra.Minimum = 0
					Me.Barra.Value1 = 0

					For p As Integer = 0 To Me.Tabla2.RowCount - 1

						If p >= 1 Then
							If Me.Tabla2.Item(UUIDR.Index, p).Value = Me.Tabla2.Item(UUIDR.Index, p - 1).Value Then
							Else
								Dim T As Integer = 1
								Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (convert(INT,Num_Pol))+1 FROM Polizas WHERE Id_Empresa= 1 AND id_Anio = '" & Me.ComboAño.Text.Trim() & "' AND id_Mes ='" & Me.Tabla2.Item(Mesc.Index, p).Value & "' and Id_Tipo_Pol_Sat = " & 15 & "")
								If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
									Numero_Polzia = 1
								Else
									Numero_Polzia = ds.Tables(0).Rows(0)(0)
								End If
							End If
						Else

							Dim T As Integer = 1
							Dim ds As DataSet = Eventos.Obtener_DS("SELECt max (convert(INT,Num_Pol))+1 FROM Polizas WHERE Id_Empresa= 1 AND id_Anio = '" & Me.ComboAño.Text.Trim() & "' AND id_Mes ='" & Me.Tabla2.Item(Mesc.Index, p).Value & "' and Id_Tipo_Pol_Sat = " & 15 & "")
							If IsDBNull(ds.Tables(0).Rows(0)(0)) = True Then
								Numero_Polzia = 1
							Else
								Numero_Polzia = ds.Tables(0).Rows(0)(0)
							End If
						End If

						Id = RegresaIdC(Me.Tabla2.Item(UUIDR.Index, p).Value, "Id_Xml_Complemento", "Xml_Complemento")


                        Codificar_polizasComplementos(p, Me.ComboAño.Text.Trim(), Me.Tabla2.Item(Mesc.Index, p).Value.ToString, Me.Tabla2.Item(Fecha_EmisionC.Index, p).Value.ToString.Substring(0, 2), Me.Tabla2.Item(Fecha_TimbradoC.Index, p).Value,
                                     "I S/Factura/" & Me.Tabla2.Item(UUIDR.Index, p).Value, Me.Tabla2.Item(UUIDR.Index, p).Value, Numero_Polzia, Id, Me.Tabla2.Item(RFCrC.Index, p).Value,
                                  Me.Tabla2.Item(Imp_TrasnfC.Index, p).Value, Me.Tabla2.Item(NombreRC.Index, p).Value, Me.Tabla2.Item(GrabadoC.Index, p).Value, Me.Tabla2.Item(ExentoC.Index, p).Value, Me.Tabla2.Item(IVARc.Index, p).Value,
                                  Me.Tabla2.Item(BancoO.Index, p).Value, Me.Tabla2.Item(BancoDestino.Index, p).Value, Me.Tabla2.Item(Cta_Destino.Index, p).Value, Me.Tabla2.Item(Cta_Origen.Index, p).Value,
                                              Me.Tabla2.Item(CtaBancoCat.Index, p).Value, Me.Tabla2.Item(UUIDR.Index, p).Value, 15, Me.Tabla2.Item(IVAA6C.Index, p).Value)

                        If Me.Barra.Value1 = Me.Barra.Maximum Then
							Me.Barra.Minimum = 0
							Me.Cursor = Cursors.Arrow
							RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
							Me.Barra.Value1 = 0
							Me.Barra.Visible = False
						Else
							Me.Barra.Value1 += 1
						End If
					Next
				End If

			Else
				RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			End If
		End If


	End Sub
	Private Function Checa_tipo(ByVal tipo As String, ByVal cliente As Integer)
		Dim clave As String = ""
		Dim sql As String = "SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE ID_Empresa= " & cliente & " AND clave = '" & tipo.Substring(0, 3) & "'"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			clave = ds.Tables(0).Rows(0)("Id_Tipo_Pol_Sat")
		Else
			clave = 0
		End If
		Return clave
	End Function
	Private Function BuscaR_CFDI_Conta(ByVal UUID As String, ByVal Tabla As String, ByVal Campo As String)
		Dim hacer As Boolean = True
		Dim sql As String = "SELECT * FROM " & Tabla & " WHERE ID_poliza IS not NULL AND " & Campo & " ='" & UUID & "'"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		End If
		Return hacer
	End Function

	Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
		If Me.TablaConceptos.SelectedIndex = 0 Then
			If Me.Tabla1.Rows.Count > 0 Then
				Me.Tabla1.Rows.Clear()
				Me.lblRegistros.Text = "Total de Registros: "
			End If
		Else
			If Me.Tabla2.Rows.Count > 0 Then
				Me.Tabla2.Rows.Clear()
				Me.lblRegistros.Text = "Total de Registros: "
			End If
		End If

	End Sub
	Private Function Calcula_poliza(ByVal Anio As Integer, ByVal Mes As String, ByVal Tipo As Integer)

		Dim poliza As String = Eventos.Num_polizaS(1, Tipo, Anio, Mes, Tipo)

		Return poliza
	End Function
	Private Sub Edita_Factura(ByVal Folio_Fiscal As String, ByVal detaclle As String, ByVal Poliza As String)
		Dim sql As String = " UPDATE dbo.Facturas
                        SET ID_poliza = '" & Poliza & "'
                        WHERE Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "' "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("EditaFacturas", sql)

		End If
	End Sub
	Private Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String)
		Dim hacer As Boolean
		Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Sub Inserta_Comprobante_Fiscal(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
						   ByVal Rfc_Emisor As String, ByVal fecha As String,
						   ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Decimal)
		Dim sql As String = "INSERT INTO dbo.Facturas"
		sql &= " 	(                   "
		sql &= " 	ID_anio,                    "
		sql &= " 	ID_mes,                     "
		sql &= " 	ID_poliza,                  "
		sql &= " 	RFC_Emisor,                 "
		sql &= " 	Folio_Fiscal,               "
		sql &= " 	Referencia,                 "
		sql &= " 	Importe,                "
		sql &= " 	Fecha_Comprobante,          "
		sql &= " 	Detalle_Comp_Electronico,ID_Empresa"
		sql &= "    )                         "
		sql &= " VALUES "
		sql &= "(                             "
		sql &= " '" & anio & "',	" '@id_anio,                   
		sql &= " '" & mes & "'," '@id_mes,                    
		sql &= " '" & id_poliza & "'," '@id_poliza,                 
		sql &= " '" & Rfc_Emisor & "'," '@rfc_emisor,                
		sql &= " '" & Folio_Fiscal & "'," '@folio_fiscal,              
		sql &= " '" & Referencia & "'," '@referencia,                
		sql &= " " & Importe & "	," '@importe,                   
		sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_comprobante,         
		sql &= " 'C', 1 " '@detalle_comp_electronico   
		sql &= " )"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("InsertarFacturas", sql)

		End If
	End Sub
	Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
						   ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
						   ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
		Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino,id_Empresa	) VALUES	("

		sql &= " '" & anio & "',	" '@id_anio,                   
		sql &= " '" & mes & "'," '@id_mes,     
		sql &= " '" & tipo & "'," '@tipo    

		sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
		sql &= " '" & No_cheque & "'," '@no_cheque,  
		sql &= " '" & no_banco & "'," '@no_banco,  
		sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
		sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
		sql &= " " & Importe & "	," '@importe,                    
		sql &= " '" & id_poliza & "', " '@id_poliza,  
		sql &= " 'T','" & Trim(bancoD) & "', '" & Trim(cuentaD.Replace("/", "")) & "',1" '@tipo_ce, 
		sql &= " )"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("InsertarCeT", sql)
		End If
	End Sub
    Private Sub Codificar_polizas(ByVal posicion As Integer, ByVal Anio As String, ByVal Mes As String, ByVal Dia As String, ByVal Fecha_Emision As String,
                                  ByVal Leyenda As String, ByVal UUID As String, ByVal Numpol As Integer, ByVal id As Integer, ByVal RFC As String,
                                  ByVal ImpProvision As Decimal, ByVal Nombre As String, ByVal ImpG As Decimal, ByVal ImpEx As Decimal,
                                  ByVal IvaR As Decimal, ByVal Tipo As Integer, ByVal IvaR6 As Decimal)
        Dim poliza_Sistema As String = ""
        '  If Me.Tabla1.Item(Psistema.Index, posicion).Value <> "" Then ' quitar linea despues del desbloqueo
        poliza_Sistema = Calcula_poliza(Anio, Mes, Tipo)
        '  End If

        'Calcular consecutivo poliza
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))

        If Creapoliza(poliza_Sistema, Anio, Mes, Dia,
                   consecutivo, Tipo,
                  Fecha_Emision, Leyenda, "Auto", Numpol, id, False) = True Then

            If Buscafactura(Trim(UUID), "C") = True Then
                'Se inserta la Factura
                Inserta_Comprobante_Fiscal(poliza_Sistema, Anio, Mes,
                              RFC, Fecha_Emision,
                              UUID, "Factura " & Trim(RFC) & " C", ImpProvision)
            Else
                'Se Edita la Factura
                Edita_Factura(UUID, "C", poliza_Sistema)
            End If

            Crear_detalle(posicion, poliza_Sistema, ImpProvision, RFC, Nombre, ImpG, ImpEx, IvaR, IvaR6)
        End If
    End Sub
    Private Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer,
                                ByVal registro As Integer, Optional ByVal comple As Boolean = False)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
        sql &= "     ID_anio,        "
        sql &= "     ID_mes,        "
        sql &= "     ID_dia,        "
        sql &= "     consecutivo,    "
        sql &= "     Num_Pol,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     ID_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario,Aplicar_Poliza         "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,     
        sql &= " 	'" & dia & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,   
        sql &= " 	" & num_pol & "," '@num_pol,  
        sql &= " 	" & tipo & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & 1 & "," '@ID_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
            Eventos.Insertar_usuariol("InsertarPolizCp", sql)
            If comple = True Then
                Actualiza_RegistroC(id_poliza, registro)
            Else
                Actualiza_Registro(id_poliza, registro)
            End If
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Function Regresa_Cuenta_Series(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String)
		Dim cta As String = ""
		Dim sql As String = "SELECT Id_Ingreso,	Serie,	Abono,	CtaIngG,	CtaIngEx,	CtaIngC,	IVATras,	ISRRet,	IVARet,	CtaIngPCG,	CtaIngPCE,	CtaIngPCC,
	IVAPTras,	ISRRPA,	IVARetPA,ID_Empresa,Clientes FROM dbo.Series WHERE ID_Empresa = " & cliente & " and Serie = '" & serie & "'"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			cta = Trim(ds.Tables(0).Rows(0)(tipo)).Substring(0, 12)
		Else

			cta = ""
		End If
		Return cta
	End Function
	Private Function RegresaCuneta(ByVal cuenta As String, ByVal rfc As String, ByVal posicion As Integer, ByVal Nombre As String, Optional ByVal tipo As Integer = 0)
		Dim Cta As String = ""
		Dim sql As String = ""
		If tipo = 1 Then
			sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 > 0 AND RFC = '" & rfc & "' and ID_Empresa = " & 1 & " "
		ElseIf tipo = 2 Then

			sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and ID_Empresa = " & 1 & " "
		ElseIf tipo = 0 Then

			sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 >= 0 AND RFC = '" & rfc & "' and ID_Empresa = " & 1 & " "

		End If

		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			Cta = ds.Tables(0).Rows(0)(0)
		Else
			'No existe la cuenta y la inserta
			If tipo = 1 Then
				Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0 and ID_Empresa = " & 1 & "", True))
				Cta = Format(Cta).PadLeft(4, "0")
				Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
							   "0000", cuenta.Substring(0, 8) & Cta & "0000", rfc & " " & Nombre,
							   1, "IPC", rfc)
				Cta = cuenta.Substring(0, 8) & Cta & "0000"
			ElseIf tipo = 0 Then
				Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and ID_Empresa = " & 1 & "", True))
				Cta = Format(Cta).PadLeft(4, "0")
				Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
								  Cta, cuenta.Substring(0, 12) & Cta, rfc & " " & Nombre,
								  1, "IPC", rfc)
				Cta = cuenta.Substring(0, 12) & Cta
			ElseIf tipo = 2 Then
				Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 >= 0000 and ID_Empresa = " & 1 & "", True))
				Cta = Format(Cta).PadLeft(4, "0")
				Crear_cuenta(cuenta.ToString.Substring(0, 4), Cta.ToString.Substring(0, 4), "0000",
								 "0000", cuenta.Substring(0, 4) & Cta & "00000000", rfc & " " & Nombre,
								 1, "IPC", rfc)
				Cta = cuenta.Substring(0, 4) & Cta & "00000000"
			End If
		End If
		Return Cta
	End Function
	Private Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
							 ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, ByVal letra As String, ByVal RFC As String)
		Dim ds As DataSet = Eventos.Obtener_DS("Select Naturaleza,Clasificacion,Balanza,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and ID_Empresa = " & cliente & "  ")

		If ds.Tables(0).Rows.Count > 0 Then
			Dim sql As String = ""
			sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
			sql &= "("
			sql &= "Nivel1, "
			sql &= "Nivel2,"
			sql &= "Nivel3,"
			sql &= "Nivel4,"
			sql &= "Cuenta,"
			sql &= "Descripcion,"
			sql &= "Naturaleza,"
			sql &= "Clasificacion,"
			sql &= "Codigo_Agrupador,"
			If RFC <> "" Then
				sql &= "RFC,"
			Else
				sql &= "RFC,"
			End If
			sql &= "ID_Empresa,clave, "
			sql &= "Balanza,"
			sql &= "Cta_ceros,  "
			sql &= "Cta_Cargo_Cero,"
			sql &= "Cta_Abono_Cero  "

			sql &= "	)  "
			sql &= "VALUES  "
			sql &= "	(  "
			sql &= "	'" & nivel1 & "'," '@nivel1
			sql &= "	'" & nivel2 & "'," '@nivel2
			sql &= "	'" & nivel3 & "'," '@nivel3
			sql &= "	'" & nivel4 & "'," '@nivel4
			sql &= "	'" & cuenta & "'," '@cuenta
			sql &= "	'" & descripcion & "'," '@descripcion
			sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Naturaleza")) & "'," '@naturaleza
			sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Clasificacion")) & "'," '@clasificacion
			Dim su As String = ""
			If (nivel4 <> "0000" Or nivel3 <> "0000") And nivel2 <> "0000" Then
				su = nivel3.Substring(2, 2)
			End If
			sql &= "	'" & nivel1.ToString.Substring(0, 3) & su & "'," '@codigo_agrupador
			If RFC = "" Then
				sql &= "	NULL," '@RFC
			Else
				sql &= "	'" & RFC & "'," '@RFC
			End If
			sql &= "	" & cliente & " , '" & Trim(letra) & "'," '@ID_Empresa    

			sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Balanza"))) & "," '@Balanza
			sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_ceros"))) & "," '@Cta_ceros
			sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Cargo_Cero"))) & "," '@Balanza
			sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Abono_Cero"))) & "" '@Cta_ceros
			sql &= "  )"
			' Ingresar codigo para importar catalogos
			If Eventos.Comando_sql(sql) > 0 Then
				Eventos.Insertar_usuariol("Crear_CtaCarga", sql)
			End If
		Else

		End If
	End Sub
	Private Function Regresa_Cuenta_Impuestos(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String)
		Dim cta As String = ""
		Dim sql As String = "SELECT 
	Id_Ingreso,	Serie,	Abono,	CtaIngG,	CtaIngEx,	CtaIngC,	IVATras,	ISRRet,	IVARet,	CtaIngPCG,	CtaIngPCE,	CtaIngPCC,
	IVAPTras,	ISRRPA,	IVARetPA, 	ID_Empresa FROM dbo.Series WHERE ID_Empresa = " & cliente & " and Serie = '" & serie & "' "
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			cta = Trim(ds.Tables(0).Rows(0)(tipo))
		Else

			cta = ""
		End If
		Return cta
	End Function
    Private Sub Crear_detalle(ByVal p As Integer, ByVal pol As String, ByVal ImpProvis As Decimal,
                              ByVal RFC As String, ByVal Nombre As String, ByVal ImpG As Decimal,
                              ByVal ImpEx As Decimal, ByVal IvaR As Decimal, ByVal Ivar6 As Decimal)
        Dim Item As Integer = 1

        Dim cadena As String = "" 'Trim(Me.Tabla1.Item(NCuenta.Index, p).Value)
        Dim posi As Integer = 0 ' InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = 0 'Len(cadena) - Len(cadena.Substring(0, posi))
        Dim Cuenta_Cargo As String = "" 'cadena.Substring(posi, cuantos)
        Dim Cuenta2 As String = ""
        Cuenta_Cargo = RegresaCuneta(Regresa_Cuenta_Series(1, "Clientes", "CFDI"), Trim(RFC), p, Nombre, 1)
        Crea_detalle_poliza(pol, Item, ImpProvis, 0, Cuenta_Cargo, "")
        Item = Item + 1
        Crea_detalle_poliza(pol, Item, Ivar6, 0, "1130000100040000", "")
        Item = Item + 1
        If ImpEx > 0 And ImpG > 0 And IvaR > 0 Then ' tiene grabado y exento
            Cuenta_Cargo = RegresaCuneta(Regresa_Cuenta_Series(1, "CtaIngPCG", "CFDI"), Trim(RFC), p, Nombre, 1)
            Cuenta2 = RegresaCuneta(Regresa_Cuenta_Series(1, "CtaIngPCE", "CFDI"), Trim(RFC), p, Nombre, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpG, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, ImpEx, Cuenta2, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, IvaR, Regresa_Cuenta_Impuestos(1, "IVAPTras", "CFDI"), "")
            Item = Item + 1
        ElseIf ImpEx > 0 And ImpG <= 0 Then 'Tiene Exento o 0 
            Cuenta_Cargo = RegresaCuneta(Regresa_Cuenta_Series(1, "CtaIngPCE", "CFDI"), Trim(RFC), p, Nombre, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpEx, Cuenta_Cargo, "")
            Item = Item + 1
        ElseIf ImpEx <= 0 And ImpG > 0 Then 'Tiene Grabado 
            Cuenta_Cargo = RegresaCuneta(Regresa_Cuenta_Series(1, "CtaIngPCG", "CFDI"), Trim(RFC), p, Nombre, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpG, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, IvaR, Regresa_Cuenta_Impuestos(1, "IVAPTras", "CFDI"), "")
            Item = Item + 1
        End If

        Exit Sub

    End Sub
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
									   ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
		Dim sql As String = ""
		sql &= "         INSERT INTO dbo.Detalle_Polizas"
		sql &= "(   "
		sql &= " ID_poliza,      "
		sql &= " ID_item,       "
		sql &= " Cargo,          "
		sql &= " Abono,         "
		sql &= " Fecha_captura,  "
		sql &= " Movto,"
		sql &= " Cuenta, "
		sql &= " No_cheque  "
		sql &= " ) "
		sql &= " VALUES "
		sql &= "( "
		sql &= " '" & id_poliza & "'	," '@id_poliza,     
		sql &= "" & item & "," '@id_item,       
		sql &= "" & cargo & "," '@cargo,         
		sql &= "" & Abono & "," '@abono,         
		sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
		sql &= " 'A'	," '@movto,         
		sql &= " " & cuenta & "	," '@cuenta,        
		sql &= " '" & cheque & "'" '@no_cheque      
		sql &= " 	)"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("InsertarPolizD", sql)
		End If
	End Sub
    Private Sub Crear_detalleC(ByVal p As Integer, ByVal pol As String, ByVal ImpTComplemento As Decimal,
                               ByVal RFC As String, ByVal Nombre As String, ByVal ImpG As Decimal,
                               ByVal ImpEx As Decimal, ByVal IvaR As Decimal, ByVal Cta_Bancos As String,
                               ByVal uuid As String, ByVal IVAA6 As Decimal)
        Dim Cuenta_Cargo As String = ""
        Dim Cuenta2 As String = ""
        Dim Item As Integer = 1

        '1° Inserta los Bancos
        If ImpTComplemento > 0 Then ' Transferencia
            Crea_detalle_poliza(pol, Item, ImpTComplemento - IVAA6, 0, Cta_Bancos, "")
            Item = Item + 1
        End If

        Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "Clientes", "PG"), RFC.Trim(), p, Nombre, 1)
        Crea_detalle_poliza(pol, Item, 0, ImpTComplemento, Cuenta_Cargo, "")
        Item = Item + 1

        Crea_detalle_poliza(pol, Item, 0, IVAA6, "1130000100040000", "")
        Item = Item + 1

        Crea_detalle_poliza(pol, Item, IVAA6, 0, "1130000800040000", "")
        Item = Item + 1

        'Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
        'PorcenPro = ImpTComplemento / ImpFaCURA(uuid, "Total_Real")
        'ImpGpro = ImpFaCURA(uuid, "Imp_Grabado") * PorcenPro
        'ImpEpro = ImpFaCURA(uuid, "Imp_Exento") * PorcenPro
        'ImpIvaP = ImpFaCURA(uuid, "IVA_real") * PorcenPro



        If ImpEx > 0 And ImpG > 0 And IvaR > 0 Then ' tiene grabado y exento

            Cuenta2 = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngPCE", "PG"), Trim(RFC), p, 1, 1)
            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngPCG", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, ImpG, 0, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, ImpEx, 0, Cuenta2, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, IvaR, 0, Regresa_Cuenta_Impuestos(1, "IVAPTras", "PG"), "")

            Item = Item + 1

        ElseIf ImpEx > 0 And ImpG <= 0 Then 'Tiene Exento o 0

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngPCE", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, ImpEx, 0, Cuenta_Cargo, "")
            Item = Item + 1

        ElseIf ImpEx <= 0 And ImpG > 0 Then 'Tiene Grabado

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngPCG", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, ImpG, 0, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, IvaR, 0, Regresa_Cuenta_Impuestos(1, "IVAPTras", "PG"), "")

            Item = Item + 1

        End If
        'Cargos con cuentas efectivo bancos
        If ImpEx > 0 And ImpG > 0 And IvaR > 0 Then ' tiene grabado y exento

            Cuenta2 = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngEx", "PG"), Trim(RFC), p, 1, 1)
            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngG", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpG, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, ImpEx, Cuenta2, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, IvaR, Regresa_Cuenta_Impuestos(1, "IVATras", "PG"), "")

            Item = Item + 1

        ElseIf ImpEx > 0 And ImpG <= 0 Then 'Tiene Exento o 0

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngEx", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpEx, Cuenta_Cargo, "")
            Item = Item + 1

        ElseIf ImpEx <= 0 And ImpG > 0 Then 'Tiene Grabado

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(1, "CtaIngG", "PG"), Trim(RFC), p, 1, 1)
            Crea_detalle_poliza(pol, Item, 0, ImpG, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, IvaR, Regresa_Cuenta_Impuestos(1, "IVATras", "PG"), "")

            Item = Item + 1

        End If



        Exit Sub
    End Sub
    Private Function RegresaCunetaComple(ByVal cuenta As String, ByVal rfc As String, ByVal posicion As Integer, ByVal Nombre As String, Optional ByVal tipo As Integer = 0)
		Dim Cta As String = ""
		Dim sql As String = ""

		If tipo = 1 Then
			sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 > 0 AND RFC = '" & rfc & "' and ID_Empresa = " & 1 & " "
		Else
			sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and ID_Empresa = " & 1 & " "
		End If
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			Cta = ds.Tables(0).Rows(0)(0)
		Else
			'No existe la cuenta y la inserta
			If tipo = 1 Then
				Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and ID_Empresa = " & 1 & "", True))
				Cta = Format(Cta).PadLeft(4, "0")
				Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
							   "0000", cuenta.Substring(0, 8) & Cta & "0000", rfc & " " & Nombre, 1, "I", rfc)
				Cta = cuenta.Substring(0, 8) & Cta & "0000"
			Else
				Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 > 0 and ID_Empresa = " & 1 & "", True))
				Cta = Format(Cta).PadLeft(4, "0")
				Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
								  Cta, cuenta.Substring(0, 12) & Cta, rfc & " " & Nombre, 1, "I", rfc)
				Cta = cuenta.Substring(0, 12) & Cta
			End If
		End If
		Return Cta
	End Function
	Private Function ImpFaCURA(ByVal uuid As String, ByVal CAMPO As String)
		Dim iMP As Decimal
		Dim ds As DataSet = Eventos.Obtener_DS("SELECT " & CAMPO & " FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' AND ID_Empresa = " & 1 & "")
		If ds.Tables(0).Rows.Count > 0 Then
			iMP = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
		Else
			iMP = 0
		End If
		Return iMP
	End Function
    Private Sub Codificar_polizasComplementos(ByVal posicion As Integer, ByVal Anio As String, ByVal Mes As String, ByVal Dia As String, ByVal Fecha_Emision As String,
                                  ByVal Leyenda As String, ByVal UUID As String, ByVal Numpol As Integer, ByVal id As Integer, ByVal RFC As String,
                                  ByVal ImpT As Decimal, ByVal Nombre As String, ByVal ImpG As Decimal, ByVal ImpEx As Decimal, ByVal IvaR As Decimal,
                                  ByVal BancoOrigen As String, ByVal BancoDestino As String, ByVal CtaBD As String, ByVal CuentaO As String,
                                              ByVal Cuenta_Bancos As String, ByVal UUIDR As String, ByVal Tipo As String, ByVal IvaA6 As Decimal)
        Dim poliza_Sistema As String = ""
        '  If Me.TablaImportar.Item(Psistema.Index, posicion).Value <> "" Then ' quitar linea despues del desbloqueo
        poliza_Sistema = Calcula_poliza(Anio, Mes, Tipo)
        '  End If

        'Calcular consecutivo poliza
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))

        If Creapoliza(poliza_Sistema, Anio, Mes, Dia,
                   consecutivo, Tipo,
                  Fecha_Emision, Leyenda, "Auto", Numpol, id, True) = True Then

            If Buscafactura(Trim(UUID), "C") = True Then
                'Se inserta la Factura
                Inserta_Comprobante_Fiscal(poliza_Sistema, Anio, Mes,
                              RFC, Fecha_Emision,
                              UUID, "Factura " & Trim(RFC) & " C", ImpT)
            Else
                'Se Edita la Factura
                Edita_Factura(UUID, "C", poliza_Sistema)
            End If

            If ImpT > 0 Then
                Dim cadena As String = BancoOrigen
                Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                Dim BO As String = cadena.Substring(0, posil - 1)

                cadena = BancoDestino
                posil = InStr(1, cadena, "-", CompareMethod.Binary)
                Dim Bd As String = cadena.Substring(0, posil - 1)

                Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Anio, Mes, RFC, "004", Fecha_Emision, "", BO, CuentaO, UUID, ImpT, Bd, CtaBD)
            End If
            Crear_detalleC(posicion, poliza_Sistema, ImpT, RFC, Nombre, ImpG, ImpEx, IvaR, Cuenta_Bancos, UUIDR, IvaA6)
        End If

    End Sub
    Private Sub Actualiza_Registro(ByVal poliza As String, ByVal registro As Integer)
		Dim sql As String = " UPDATE dbo.xml_sat
                        SET ID_poliza = '" & poliza & "'
                        WHERE Id_Registro_Xml = " & registro & "  "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Auto", sql)
		End If

	End Sub
	Private Sub Actualiza_RegistroC(ByVal poliza As String, ByVal registro As Integer)
		Dim sql As String = " UPDATE dbo.Xml_Complemento
                        SET ID_poliza = '" & poliza & "'
                        WHERE Id_Xml_Complemento = " & registro & "  "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Auto", sql)
		End If

	End Sub

	Private Sub Contabilizacion_Digital_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTablaEnca(Me.Tabla1)
        Eventos.DiseñoTablaEnca(Me.Tabla2)

        Eventos.DiseñoTablaEnca(Me.TablaD)

        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
			If i >= 2010 Then
				Me.ComboAño.Items.Add(Str(i))
			End If
		Next
		Me.ComboAño.Text = Str(DateTime.Now.Year)
		Dim mes = Now.Date.Month.ToString

		If Len(mes) < 2 Then
			mes = "0" & mes
		End If
		Me.ComboMesIn.Text = "01"
		Me.ComboMesFin.Text = mes

		Dim sql As String = " SELECT Bancos.Clave,rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias 
                            FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  
                            where ID_Empresa = 1 and alias like '%Transf%'"

		Me.LstBancos.Cargar(sql)
		Me.LstBancos.SelectItem = "14"
	End Sub

	Private Sub CmdAsignaBoveda_Click(sender As Object, e As EventArgs) Handles CmdAsignaBoveda.Click



		Dim archivo As String = InputBox("Teclea la ruta de la Boveda:", Eventos.titulo_app, My.Computer.FileSystem.SpecialDirectories.MyDocuments)

		If archivo <> "" Then
			Dim sql As String = "UPDATE dbo.Usuarios SET Boveda = '" & archivo & "' WHERE (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')"
			If Eventos.Comando_sql(sql) > 0 Then
				Eventos.Insertar_usuariol("DigitalE", sql)
			End If
		Else
			Exit Sub
		End If

	End Sub
	Private Function Banco_destino(ByVal Cliente As Integer, ByVal Rfc As String)
		Dim banco As String = ""
		Dim sql2 As String = " SELECT DISTINCT CONVERT(NVARCHAR, Bancos.clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE emitidas=1 and (Bancos_RFC.ID_Empresa  = " & Cliente & ") and RFC = '" & Rfc & "' and Favorito=1"
		Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)

		If ds2.Tables(0).Rows.Count > 0 Then
			Dim largo As Integer = Len(ds2.Tables(0).Rows(0)("Clabe"))
			Try
				banco = Trim(ds2.Tables(0).Rows(0)("Banco")) & " " & Trim(ds2.Tables(0).Rows(0)("Clabe").ToString.Substring(largo - 3, 3))
			Catch ex As Exception

			End Try
		Else
			banco = ""
		End If

		Return banco
	End Function

	Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
		If Me.TablaConceptos.SelectedIndex = 0 Then

			Dim contador As Integer = 0
			Dim archivo As String = ""
			Dim Posicion As Integer = 0
			archivo = Eventos.Boveda(Inicio.LblUsuario.Text)
			Dim ruta As String
			Dim tipo As String = ""
			If Me.ComboMesIn.Text <> "" And Me.ComboMesFin.Text <> "" Then
				If Me.ComboMesIn.Text <= Me.ComboMesFin.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.ComboMesFin.Text) - Convert.ToInt32(Me.ComboMesIn.Text)
                    Dim meces(Tam) As String
                    For i As Integer = Convert.ToInt32(Me.ComboMesIn.Text) - 1 To Convert.ToInt32(Me.ComboMesIn.Text) + Tam - 1
                        meces(contador) = Me.ComboMesFin.Items(i).ToString()
                        contador = contador + 1
                    Next
                    Me.Cursor = Cursors.AppStarting
					Me.Barra.Maximum = meces.Length - 1
					Me.Barra.Minimum = 0
					Me.Barra.Value1 = 0
					For Each Va As String In meces
						Dim cadena As String = "\AMA9611062U0\Emitidas\" & Trim(Me.ComboAño.Text) & "\" & Va
						ruta = archivo & cadena
						If archivo = "" Then
							MessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Error)
							Exit Sub
						Else
							Try

								For Each item As String In Directory.GetFiles(ruta, "*.xml", False)

									Dim nombre As String = My.Computer.FileSystem.GetName(item)

									Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)
									If sa.Tables(0).Rows(0)("TipoDeComprobante") = "I" Then
										If BuscaR_CFDI_Conta(sa.Tables("TimbreFiscalDigital").Rows(0)("UUID"), "Xml_Sat", "UUID") = True Then

											Dim tiene As Boolean = False
											For i As Integer = 0 To Me.Tabla1.RowCount - 1
												If Me.Tabla1.Item(Tip.Index, i).Value <> Nothing Then
													tiene = True
												End If
											Next
											If tiene = False Then
												Me.Tabla1.Rows.Add()
											End If
											If sa.Tables(0).Rows.Count > 0 Then

												Me.Tabla1.Item(FechaEmision.Index, Posicion).Value = Convert.ToDateTime(sa.Tables(0).Rows(0)("Fecha")).ToString("dd/MM/yyyy")
												Me.Tabla1.Item(FechaTimbrado.Index, Posicion).Value = Convert.ToDateTime(sa.Tables("TimbreFiscalDigital").Rows(0)("FechaTimbrado")).ToString("dd/MM/yyyy")
												Try
													Me.Tabla1.Item(Mes.Index, Posicion).Value = Me.Tabla1.Item(FechaEmision.Index, Posicion).Value.ToString.Substring(3, 2)
												Catch ex As Exception
													Me.Tabla1.Item(Mes.Index, Posicion).Value = ""
												End Try
												Try
													Me.Tabla1.Item(Serie.Index, Posicion).Value = sa.Tables(0).Rows(0)("Serie")
												Catch ex As Exception
													Me.Tabla1.Item(Serie.Index, Posicion).Value = ""
												End Try
												Try
													Me.Tabla1.Item(Folio.Index, Posicion).Value = sa.Tables(0).Rows(0)("Folio")
												Catch ex As Exception
													Me.Tabla1.Item(Folio.Index, Posicion).Value = ""
												End Try

												Me.Tabla1.Item(UUID.Index, Posicion).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")
												Try
													Me.Tabla1.Item(RFCEmisor.Index, Posicion).Value = sa.Tables(1).Rows(0)("Rfc")
												Catch ex As Exception

												End Try

												Try
													Me.Tabla1.Item(NombreEmisor.Index, Posicion).Value = sa.Tables(1).Rows(0)("Nombre")
												Catch ex As Exception
													Me.Tabla1.Item(NombreEmisor.Index, Posicion).Value = ""
												End Try
												Try
													Me.Tabla1.Item(RFCReceptor.Index, Posicion).Value = sa.Tables(2).Rows(0)("Rfc")
												Catch ex As Exception
													Me.Tabla1.Item(RFCReceptor.Index, Posicion).Value = ""
												End Try
												Try
													Me.Tabla1.Item(NombreReceptor.Index, Posicion).Value = sa.Tables(2).Rows(0)("Nombre")
												Catch ex As Exception
													Me.Tabla1.Item(NombreReceptor.Index, Posicion).Value = ""
												End Try
												Me.Tabla1.Item(SubTotal.Index, Posicion).Value = sa.Tables(0).Rows(0)("SubTotal")
												Try
													For f As Integer = 0 To sa.Tables("Impuestos").Rows.Count - 1
														Me.Tabla1.Item(IVA16.Index, Posicion).Value = Convert.ToDecimal(Me.Tabla1.Item(IVA16.Index, Posicion).Value) + Convert.ToDecimal(IIf(IsDBNull(sa.Tables("Impuestos").Rows(f)("TotalImpuestosTrasladados")) = True, 0, sa.Tables("Impuestos").Rows(f)("TotalImpuestosTrasladados")))
													Next

												Catch ex As Exception
													Me.Tabla1.Item(IVA16.Index, Posicion).Value = 0
												End Try
												Me.Tabla1.Item(Total.Index, Posicion).Value = sa.Tables(0).Rows(0)("Total")
												Me.Tabla1.Item(ru.Index, Posicion).Value = ruta
												Me.Tabla1.Item(nom.Index, Posicion).Value = nombre
												Me.Tabla1.Item(Grabado.Index, Posicion).Value = Math.Round(IIf(IsDBNull(Me.Tabla1.Item(IVA16.Index, Posicion).Value) = True, 0, Me.Tabla1.Item(IVA16.Index, Posicion).Value / 0.16), 2)
												Me.Tabla1.Item(Exento.Index, Posicion).Value = Math.Round(IIf(IsDBNull(Me.Tabla1.Item(SubTotal.Index, Posicion).Value) = True, 0, Me.Tabla1.Item(SubTotal.Index, Posicion).Value - Me.Tabla1.Item(Grabado.Index, Posicion).Value), 2)
												Me.Tabla1.Item(IvaR.Index, Posicion).Value = Math.Round(IIf(IsDBNull(Me.Tabla1.Item(Grabado.Index, Posicion).Value) = True, 0, Me.Tabla1.Item(Grabado.Index, Posicion).Value * 0.16), 2)
                                                Dim Retencion6 As Decimal = 0
                                                For i As Integer = 0 To sa.Tables(9).Rows.Count - 2
                                                    Retencion6 += sa.Tables("Retencion").Rows(i)("Importe")
                                                Next
                                                Me.Tabla1.Item(IVA6.Index, Posicion).Value = Retencion6
                                                If Me.Tabla1.Item(Exento.Index, Posicion).Value < 1 Then
                                                    Me.Tabla1.Item(Grabado.Index, Posicion).Value = Me.Tabla1.Item(Grabado.Index, Posicion).Value + Me.Tabla1.Item(Exento.Index, Posicion).Value
                                                    Me.Tabla1.Item(Exento.Index, Posicion).Value = 0
                                                End If
                                                Me.Tabla1.Item(TotR.Index, Posicion).Value = Me.Tabla1.Item(Grabado.Index, Posicion).Value + Me.Tabla1.Item(Exento.Index, Posicion).Value + Me.Tabla1.Item(IvaR.Index, Posicion).Value - Retencion6
                                                Me.Tabla1.Item(ImpProvision.Index, Posicion).Value = Me.Tabla1.Item(TotR.Index, Posicion).Value

                                                Posicion += 1
											End If

										End If

									End If
								Next
							Catch ex As Exception
								RadMessageBox.SetThemeName("MaterialBlueGrey")
								RadMessageBox.Show("No existe la Ruta " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
								'Me.CmdLimpiar.PerformClick()
							End Try

						End If
						If Me.Barra.Value1 = Me.Barra.Maximum Then
							Me.Barra.Minimum = 0
							Me.Cursor = Cursors.Arrow
							RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

							Me.Barra.Value1 = 0
						Else
							Me.Barra.Value1 += 1
						End If
						ruta = ""
					Next
					If Me.Tabla1.Rows.Count > 0 Then
						Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla1.Rows.Count
					End If
				Else
					RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				End If
			End If
		ElseIf Me.TablaConceptos.SelectedIndex = 1 Then


			Dim contador As Integer = 0
			Dim archivo As String = ""
			Dim indice As Integer = 0
			archivo = Eventos.Boveda(Inicio.LblUsuario.Text)
			Dim ruta As String
			Dim tipo As String = ""
			If Me.ComboMesIn.Text <> "" And Me.ComboMesFin.Text <> "" Then
                If Me.ComboMesIn.Text <= Me.ComboMesFin.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.ComboMesFin.Text) - Convert.ToInt32(Me.ComboMesIn.Text)
                    Dim meces(Tam) As String
                    For i As Integer = Convert.ToInt32(Me.ComboMesIn.Text) - 1 To Convert.ToInt32(Me.ComboMesIn.Text) + Tam - 1
                        meces(contador) = Me.ComboMesFin.Items(i).ToString()
                        contador = contador + 1
                    Next
                    Me.Cursor = Cursors.AppStarting
					Me.Barra.Maximum = meces.Length - 1
					Me.Barra.Minimum = 0
					Me.Barra.Value1 = 0
					For Each Va As String In meces



						Dim cadena As String = "\AMA9611062U0\Emitidas\" & Trim(Me.ComboAño.Text) & "\" & Va
						ruta = archivo & cadena
						If archivo = "" Then
							RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
							Exit Sub
						Else
							Try


								For Each item As String In Directory.GetFiles(ruta, "*.xml", False)
									Dim nombre As String = My.Computer.FileSystem.GetName(item)

									Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)

									If sa.Tables(0).Rows(0)("TipoDeComprobante") = "P" Then

										Dim TpC As String = Eventos.ObtenerValorDB("Tipos_Poliza_Sat", "Tipos_Poliza_Sat.Id_Tipo_Pol_Sat", " Nombre LIKE '%" & sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString.Substring(Len(sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString) - 3, 3) & "%' AND Nombre NOT LIKE '%Cheq%'", True)
										' SELECT Tipos_Poliza_Sat.Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Nombre LIKE '%620%' AND Nombre NOT LIKE '%Cheq%'
										Dim CtaBanco As String = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas INNER JOIN Bancos ON Bancos.Id_Banco= Bancos_Clientes.Id_Banco", " Catalogo_de_Cuentas.cuenta", " Bancos_Clientes.Alias LIKE '%" & sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString.Substring(Len(sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString) - 3, 3) & "%' AND Bancos_Clientes.Alias NOT LIKE '%Cheq%'", True)
										Dim TipoBanco As String = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas INNER JOIN Bancos ON Bancos.Id_Banco= Bancos_Clientes.Id_Banco", " rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias ", " Bancos_Clientes.Alias LIKE '%" & sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString.Substring(Len(sa.Tables("Pago").Rows(0)("CtaBeneficiario").ToString) - 3, 3) & "%' AND Bancos_Clientes.Alias NOT LIKE '%Cheq%'", True)


										If sa.Tables("DoctoRelacionado").Rows.Count > 0 Then

											Dim Origen As String = Banco_destino(1, sa.Tables(2).Rows(0)("Rfc"))
											Dim Cta As String = ""
											Dim Destino As String = ""
											If Origen <> "" Then
												Dim posi As Integer = InStr(1, Origen, "-", CompareMethod.Binary)
												Cta = Origen.Substring(0, posi - 1)
												Cta = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    ID_Empresa =1 and Bancos.Clave = '" & Trim(Cta) & "' and  RFC = '" & sa.Tables(2).Rows(0)("Rfc") & "'", True)
												posi = InStr(1, TipoBanco, "-", CompareMethod.Binary)
												'Dim cuantos As Integer = Len(Me.LstBancos.SelectText) - Len(Me.LstBancos.SelectText.Substring(0, posi))
												Dim cuantos As Integer = Len(TipoBanco) - Len(TipoBanco.Substring(0, posi))
												Destino = TipoBanco.Substring(posi, cuantos)
												Destino = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " ID_Empresa =1 and Alias = '" & Trim(Destino) & "'", True)

											End If
											Me.Tabla2.RowCount = Me.Tabla2.RowCount + sa.Tables("DoctoRelacionado").Rows.Count
											For J As Integer = 0 To sa.Tables("DoctoRelacionado").Rows.Count - 1

												If BuscaR_CFDI_Conta(sa.Tables("DoctoRelacionado").Rows(J)("IdDocumento"), "Xml_Complemento", "IdDocumento") = True Then
													If Me.Tabla2.Item(RfcEmisorC.Index, indice).Value = "" Then
														Try

															Try
																Me.Tabla2.Item(UUIDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("IdDocumento")
															Catch ex As Exception
																Me.Tabla2.Item(UUIDR.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(TippolC.Index, indice).Value = TpC
															Catch ex As Exception
																Me.Tabla2.Item(TippolC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(CtaBancoCat.Index, indice).Value = CtaBanco
															Catch ex As Exception
																Me.Tabla2.Item(CtaBancoCat.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(TippolC.Index, indice).Value = TpC
															Catch ex As Exception
																Me.Tabla2.Item(TippolC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(RfcEmisorC.Index, indice).Value = sa.Tables(1).Rows(0)("Rfc")
															Catch ex As Exception
																Me.Tabla2.Item(RfcEmisorC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(NombreEC.Index, indice).Value = sa.Tables(1).Rows(0)("Nombre")
															Catch ex As Exception
																Me.Tabla2.Item(NombreEC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(RFCrC.Index, indice).Value = sa.Tables(2).Rows(0)("Rfc")
															Catch ex As Exception
																Me.Tabla2.Item(RFCrC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(NombreRC.Index, indice).Value = sa.Tables(2).Rows(0)("Nombre")
															Catch ex As Exception
																Me.Tabla2.Item(NombreRC.Index, indice).Value = ""
															End Try

															Try
																Me.Tabla2.Item(UUIDC.Index, indice).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")
															Catch ex As Exception
																Me.Tabla2.Item(UUIDC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(SerieC.Index, indice).Value = sa.Tables(0).Rows(0)("Serie").ToString.Trim()
															Catch ex As Exception
																Me.Tabla2.Item(SerieC.Index, indice).Value = ""
															End Try

															Try
																Me.Tabla2.Item(FolioC.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("Folio")
															Catch ex As Exception

															End Try

															Try
																Me.Tabla2.Item(Fecha_EmisionC.Index, indice).Value = sa.Tables(0).Rows(0)("Fecha")
															Catch ex As Exception
																Me.Tabla2.Item(Fecha_EmisionC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(Fecha_EmisionC.Index, indice).Value = Convert.ToDateTime(Me.Tabla2.Item(Fecha_EmisionC.Index, indice).Value)
																Me.Tabla2.Item(Mesc.Index, indice).Value = Me.Tabla2.Item(Fecha_EmisionC.Index, indice).Value.ToString.Substring(3, 2)
															Catch ex As Exception
																Me.Tabla2.Item(Mesc.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(Fecha_TimbradoC.Index, indice).Value = Convert.ToDateTime(sa.Tables(0).Rows(0)("Fecha"))
															Catch ex As Exception
																Me.Tabla2.Item(Fecha_TimbradoC.Index, indice).Value = ""
															End Try
															Try
																Me.Tabla2.Item(BancoO.Index, indice).Value = Origen
																Me.Tabla2.Item(Cta_Origen.Index, indice).Value = Cta
																Me.Tabla2.Item(BancoDestino.Index, indice).Value = TipoBanco
																Me.Tabla2.Item(Cta_Destino.Index, indice).Value = Destino
															Catch ex As Exception
																Me.Tabla2.Item(Fecha_TimbradoC.Index, indice).Value = ""
															End Try



															Me.Tabla2.Item(RutaC.Index, indice).Value = ruta
															Me.Tabla2.Item(NombreC.Index, indice).Value = nombre
															Try
																Me.Tabla2.Item(TotC.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("ImpPagado")
                                                                Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP, Retiva6 As Decimal
                                                                PorcenPro = Me.Tabla2.Item(TotC.Index, indice).Value / ImpFaCURA(Me.Tabla2.Item(UUIDR.Index, indice).Value, "Total_Real")
																ImpGpro = ImpFaCURA(Me.Tabla2.Item(UUIDR.Index, indice).Value, "Imp_Grabado") * PorcenPro
																ImpEpro = ImpFaCURA(Me.Tabla2.Item(UUIDR.Index, indice).Value, "Imp_Exento") * PorcenPro
                                                                ImpIvaP = ImpFaCURA(Me.Tabla2.Item(UUIDR.Index, indice).Value, "IVA_real") * PorcenPro
                                                                Retiva6 = ImpFaCURA(Me.Tabla2.Item(UUIDR.Index, indice).Value, "Retenido_IVA") * PorcenPro
                                                                Me.Tabla2.Item(GrabadoC.Index, indice).Value = ImpGpro
                                                                Me.Tabla2.Item(ExentoC.Index, indice).Value = ImpEpro
																Me.Tabla2.Item(IVARc.Index, indice).Value = ImpIvaP
																Me.Tabla2.Item(TotRC.Index, indice).Value = ImpGpro + ImpEpro + ImpIvaP
                                                                Me.Tabla2.Item(IVAA6C.Index, indice).Value = Retiva6



                                                                Me.Tabla2.Item(SubC.Index, indice).Value = ImpGpro
																Me.Tabla2.Item(IVAC.Index, indice).Value = ImpIvaP
																Me.Tabla2.Item(Imp_TrasnfC.Index, indice).Value = Me.Tabla2.Item(TotRC.Index, indice).Value

															Catch ex As Exception

															End Try

															Try
																'Datos destino

															Catch ex As Exception

															End Try



															indice = indice + 1
														Catch ex As Exception

														End Try
													End If
												Else
													Me.Tabla2.RowCount = Me.Tabla2.RowCount - 1
												End If

											Next

										End If
									End If ' Termina la verificacion de El tipo de comprobante
								Next
							Catch ex As Exception
								RadMessageBox.Show("No existe la Ruta" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
								'Me.CmdLimpiar.PerformClick()
							End Try

						End If
						If Me.Barra.Value1 = Me.Barra.Maximum Then
							Me.Barra.Minimum = 0
							Me.Cursor = Cursors.Arrow
							RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

							Me.Barra.Value1 = 0
						Else
							Me.Barra.Value1 += 1
						End If
						ruta = ""
					Next
					If Me.Tabla2.Rows.Count > 0 Then
						Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla2.Rows.Count
					End If
				Else
					RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				End If
			End If
		ElseIf Me.TablaConceptos.SelectedIndex = 2 Then
			Dim contador As Integer = 0
			Dim archivo As String = ""
			Dim indice As Integer = 0
			archivo = Eventos.Boveda(Inicio.LblUsuario.Text)
			Dim ruta As String
			Dim tipo As String = ""
			If Me.ComboMesIn.Text <> "" And Me.ComboMesFin.Text <> "" Then
				If Me.ComboMesIn.Text <= Me.ComboMesFin.Text Then ' seleccion correcta del periodo
					Dim Tam As Integer = Convert.ToInt32(Me.ComboMesFin.Text) - Convert.ToInt32(Me.ComboMesIn.Text)
					Dim meces(Tam) As Object
					For i As Integer = Convert.ToInt32(Me.ComboMesIn.Text) - 1 To Convert.ToInt32(Me.ComboMesIn.Text) + Tam - 1

						meces(contador) = Me.ComboMesFin.Items(i)
						contador = contador + 1
					Next
					Me.Cursor = Cursors.AppStarting
					Me.Barra.Maximum = meces.Length - 1
					Me.Barra.Minimum = 0
					Me.Barra.Value1 = 0
					For Each Va As String In meces



						Dim cadena As String = "\AMA9611062U0\Emitidas\" & Trim(Me.ComboAño.Text) & "\" & Va
						ruta = archivo & cadena
						If archivo = "" Then
							RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
							Exit Sub
						Else
							Try


								For Each item As String In Directory.GetFiles(ruta, "*.xml", False)
									Dim nombre As String = My.Computer.FileSystem.GetName(item)

									Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)

									If sa.Tables(0).Rows(0)("TipoDeComprobante") = "E" Then


										If sa.Tables("CfdiRelacionado").Rows.Count > 0 Then

											Me.TablaD.RowCount = Me.TablaD.RowCount + sa.Tables("CfdiRelacionado").Rows.Count
											For J As Integer = 0 To sa.Tables("CfdiRelacionado").Rows.Count - 1

												If BuscaR_CFDI_Conta(sa.Tables("CfdiRelacionado").Rows(J)("UUID"), "Xml_Sat", "UUID") = True Then
													If Me.TablaD.Item(RFCED.Index, indice).Value = "" Then
														Try

															Try
																Me.TablaD.Item(UUIDRN.Index, indice).Value = sa.Tables("CfdiRelacionado").Rows(J)("UUID")
															Catch ex As Exception
																Me.TablaD.Item(UUIDRN.Index, indice).Value = ""
															End Try


															Try
																Me.TablaD.Item(RFCED.Index, indice).Value = sa.Tables(4).Rows(0)("Rfc")
															Catch ex As Exception
																Me.TablaD.Item(RFCED.Index, indice).Value = ""
															End Try
															Try
																Me.TablaD.Item(NomEd.Index, indice).Value = sa.Tables(4).Rows(0)("Nombre")
															Catch ex As Exception
																Me.TablaD.Item(NomEd.Index, indice).Value = ""
															End Try

															Try
																Me.TablaD.Item(UUIDD.Index, indice).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")
															Catch ex As Exception
																Me.TablaD.Item(UUIDD.Index, indice).Value = ""
															End Try
															Try
																Me.TablaD.Item(SerD.Index, indice).Value = sa.Tables(0).Rows(0)("Serie").ToString.Trim()
															Catch ex As Exception
																Me.TablaD.Item(SerD.Index, indice).Value = ""
															End Try

															Try
																Me.TablaD.Item(FolioN.Index, indice).Value = sa.Tables(0).Rows(0)("Folio").ToString.Trim()
															Catch ex As Exception

															End Try

															Try
																Me.TablaD.Item(FechaED.Index, indice).Value = sa.Tables(0).Rows(0)("Fecha")
															Catch ex As Exception
																Me.TablaD.Item(FechaED.Index, indice).Value = ""
															End Try
															Try
																Me.TablaD.Item(FechaED.Index, indice).Value = Convert.ToDateTime(Me.TablaD.Item(FechaED.Index, indice).Value)
																Me.TablaD.Item(MesCD.Index, indice).Value = Me.TablaD.Item(FechaED.Index, indice).Value.ToString.Substring(3, 2)
															Catch ex As Exception
																Me.TablaD.Item(MesCD.Index, indice).Value = ""
															End Try
															Try
																Me.TablaD.Item(FechaTimbreN.Index, indice).Value = Convert.ToDateTime(sa.Tables("TimbreFiscalDigital").Rows(0)("FechaTimbrado"))
															Catch ex As Exception
																Me.TablaD.Item(FechaTimbreN.Index, indice).Value = ""
															End Try
															Me.TablaD.Item(SubD.Index, indice).Value = sa.Tables(0).Rows(0)("SubTotal")

															Try
																For f As Integer = 0 To sa.Tables("Impuestos").Rows.Count - 1
																	Me.TablaD.Item(IvaD.Index, indice).Value = Convert.ToDecimal(Me.TablaD.Item(IvaD.Index, indice).Value) + Convert.ToDecimal(IIf(IsDBNull(sa.Tables("Impuestos").Rows(f)("TotalImpuestosTrasladados")) = True, 0, sa.Tables("Impuestos").Rows(f)("TotalImpuestosTrasladados")))
																Next

															Catch ex As Exception
																Me.TablaD.Item(IvaD.Index, indice).Value = 0
															End Try
															'Try
															'    Me.TablaD.Item(BancoO.Index, indice).Value = Origen
															'    Me.TablaD.Item(Cta_Origen.Index, indice).Value = Cta
															'    Me.TablaD.Item(BancoDestino.Index, indice).Value = Me.LstBancos.SelectText
															'    Me.TablaD.Item(Cta_Destino.Index, indice).Value = Destino
															'Catch ex As Exception
															'    Me.TablaD.Item(Fecha_TimbradoC.Index, indice).Value = ""
															'End Try



															Me.TablaD.Item(RutaD.Index, indice).Value = ruta
															Me.TablaD.Item(NombreD.Index, indice).Value = nombre
															Try
																Me.TablaD.Item(TotD.Index, indice).Value = sa.Tables(0).Rows(0)("Total")


																Me.TablaD.Item(ImpGD.Index, indice).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(IvaD.Index, indice).Value) = True, 0, Me.TablaD.Item(IvaD.Index, indice).Value / 0.16), 2)
																Me.TablaD.Item(ImpEDV.Index, indice).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(SubD.Index, indice).Value) = True, 0, Me.TablaD.Item(SubD.Index, indice).Value - Me.TablaD.Item(ImpGD.Index, indice).Value), 2)
																Me.TablaD.Item(IVARD.Index, indice).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, indice).Value) = True, 0, Me.TablaD.Item(ImpGD.Index, indice).Value * 0.16), 2)

																Me.TablaD.Item(TotRC.Index, indice).Value = Me.TablaD.Item(ImpGD.Index, indice).Value + Me.TablaD.Item(ImpEDV.Index, indice).Value + Me.TablaD.Item(IVARD.Index, indice).Value


																Me.TablaD.Item(ImpDevD.Index, indice).Value = Me.TablaD.Item(TotD.Index, indice).Value

															Catch ex As Exception

															End Try

															Try
																'Datos destino

															Catch ex As Exception

															End Try



															indice = indice + 1
														Catch ex As Exception

														End Try
													End If
												Else
													Me.TablaD.RowCount = Me.TablaD.RowCount - 1
												End If

											Next

										End If
									End If ' Termina la verificacion de El tipo de comprobante
								Next
							Catch ex As Exception
								RadMessageBox.Show("No existe la Ruta" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
								'Me.CmdLimpiar.PerformClick()
							End Try

						End If
						If Me.Barra.Value1 = Me.Barra.Maximum Then
							Me.Barra.Minimum = 0
							Me.Cursor = Cursors.Arrow
							RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

							Me.Barra.Value1 = 0
						Else
							Me.Barra.Value1 += 1
						End If
						ruta = ""
					Next
					If Me.TablaD.Rows.Count > 0 Then
						Me.lblRegistros.Text = "Total de Registros: " & Me.TablaD.Rows.Count
					End If
				Else
					RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
				End If
			End If
		End If
	End Sub

	Private Sub TablaConceptos_TabIndexChanged(sender As Object, e As EventArgs) Handles TablaConceptos.TabIndexChanged
		If Me.TablaConceptos.SelectedIndex = 1 Then
			Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla1.Rows.Count
		Else
			Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla2.Rows.Count
		End If
	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs)

		Dim archivo As String = ""
		Buscador.ShowDialog()

		'With Buscador
		'    '.Reset()
		'    '.Description = "Selecciona la ruta de la bobeda"
		'    '.RootFolder = Environment.SpecialFolder.MyDocuments
		'    .ShowDialog()
		'    If .ShowDialog() = DialogResult.OK Then
		'        archivo = Buscador.SelectedPath
		'        Dim sql As String = "UPDATE dbo.Usuarios SET Boveda = '" & archivo & "' WHERE (Usuarios.Usuario LIKE '%" & Inicio.lblusuario.Text & "%')"
		'        If Eventos.Comando_sql(sql) > 0 Then
		'            Eventos.Insertar_usuariol("DigitalE", sql)

		'        End If
		'        .Dispose()
		'    Else
		'        .Dispose()
		'        Exit Sub
		'    End If
		'End With
	End Sub
End Class