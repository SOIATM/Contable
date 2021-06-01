Imports Telerik.WinControls
Imports System
Imports System.IO
Imports System.Collections

Public Class Importar_Polizas
    Dim Ctas As New List(Of Cuentas)
    Dim Po As New List(Of Polizas)
    Dim Tp As New List(Of TipoPolizas)
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
		If Me.TablaImportar.Rows.Count > 0 Then
			Me.TablaImportar.Rows.Clear()
			Me.lblRegistros.Text = "Total de Registros: "
		End If
	End Sub

	Private Sub Importar_Polizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
	Private Sub Cargar_clientes()
		Eventos.DiseñoTabla(Me.TablaImportar)
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Dim P As DataSet = Eventos.Obtener_DS("SELECT DISTINCT  Clave FROM Tipos_Poliza_Sat WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " ")
        If P.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To P.Tables(0).Rows.Count - 1
                Tp.Add(New TipoPolizas() With {.tipo = P.Tables(0).Rows(i)("Clave")})
            Next

        End If
        Me.lblRegistros.Text = "Total de Registros: "
	End Sub
    Private Sub Leer()
        Dim contador As Integer = 0
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        Dim txt As StreamReader
        With OpenFD
            .Title = "Seleccionar archivo TXT"
            .Filter = "Archivos de Excel (*.txt)|*.txt"
            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With

        Me.TablaImportar.AllowUserToAddRows = False
        txt = New StreamReader(archivo)
        While Not txt.EndOfStream()

            Dim texto As String = txt.ReadLine()
            Dim texto_anterior As String = texto
            Dim longitud As Integer = Len(texto) ' se verifica el tamaño de la cadena para decidir que tipo de reistro es
            If longitud > 50 Then '  tiene Informacion para Calcular cadena
                Dim Anio As String = texto.Substring(0, 4)
                Dim Mes As String = texto.Substring(4, 2)
                Dim Tipo As String = texto.Substring(6, 3)
                Dim Poliza As String = texto.Substring(9, 6)
                Dim Dia As String = ""
                Dim Detalle_CE As String = ""
                Dim rfc_Ce As String = ""
                Dim No_Cheque As String = ""
                Dim No_Banco As String = ""
                Dim Cuenta As String = ""
                Dim Descripcion As String = ""
                Dim Movimiento As String = ""
                Dim Cuenta_Origen As String = ""
                Dim Banco_Des As String = ""
                Dim cuenta_des As String = ""
                Dim Importe As String = ""
                Dim P_Aplicar As String = ""
                Dim Imp_Reg_Simplificado As String = ""
                Dim Nom_Cuenta As String = ""
                Dim Fecha_Mov As String = ""
                Dim Importe_Factura As String = ""
                Dim Folio_Fiscal_f As String = ""
                Dim Moneda As String = ""
                Dim Importe_cheque As String = ""
                Dim numero_comprobante As String = ""
                Dim por_ret_IVA As String = ""
                Dim por_ret_ISR As String = ""
                Dim por_ret_x As String = ""
                Dim por_ret_Y As String = ""
                Dim Tipo_Comprobante As String = ""
                Dim N As String = ""
                If longitud = 198 Then ' Titulo de la poliza
                    Dia = texto.Substring(15, 2)
                    Detalle_CE = ""
                    rfc_Ce = ""
                    No_Cheque = ""
                    No_Banco = ""
                    Cuenta = "      "
                    Descripcion = texto.Substring(37, 50)
                    Movimiento = "Titulo Poliza" 'texto.Substring(87, 1)
                    Cuenta_Origen = ""
                    Importe = texto.Substring(88, 17)
                    P_Aplicar = texto.Substring(105, 1)
                    Imp_Reg_Simplificado = ""
                    Nom_Cuenta = ""
                    Fecha_Mov = texto.Substring(169, 24)
                    Importe_Factura = ""
                    Folio_Fiscal_f = ""
                    Moneda = ""
                    Importe_cheque = ""
                    numero_comprobante = ""
                    por_ret_IVA = ""
                    por_ret_ISR = ""
                    por_ret_x = ""
                    por_ret_Y = ""
                    Tipo_Comprobante = ""
                    N = texto.Substring(197, 1)

                ElseIf longitud = 282 Then ' Cargos o Abonos
                    Dia = texto.Substring(15, 2)
                    Detalle_CE = ""
                    rfc_Ce = ""
                    No_Cheque = ""
                    No_Banco = ""
                    Cuenta = texto.Substring(17, 16)
                    Descripcion = texto.Substring(37, 50)
                    Movimiento = texto.Substring(87, 1)
                    If Movimiento = 1 Then
                        Movimiento = "Cargo"
                    Else
                        Movimiento = "Abono"
                    End If
                    Cuenta_Origen = ""
                    Importe = texto.Substring(88, 17)
                    P_Aplicar = texto.Substring(105, 1)
                    Imp_Reg_Simplificado = texto.Substring(135, 4)
                    Nom_Cuenta = texto.Substring(139, 53)
                    Fecha_Mov = texto.Substring(192, 24)
                    Importe_Factura = ""
                    Folio_Fiscal_f = ""
                    Moneda = ""
                    Importe_cheque = ""
                    numero_comprobante = ""
                    por_ret_IVA = texto.Substring(232, 4)
                    por_ret_ISR = texto.Substring(238, 4)
                    por_ret_x = texto.Substring(244, 4)
                    por_ret_Y = texto.Substring(252, 7)
                    Tipo_Comprobante = texto.Substring(260, 1)
                    N = texto.Substring(281, 1)
                ElseIf longitud = 259 Then
                    Dia = "  "
                    Detalle_CE = texto.Substring(17, 1)
                    rfc_Ce = texto.Substring(18, 13)
                    No_Cheque = texto.Substring(31, 3)
                    No_Banco = texto.Substring(51, 3)
                    Cuenta = "      "
                    Descripcion = "   "
                    If Detalle_CE = "H" Then
                        Movimiento = "Cheque"
                    ElseIf Detalle_CE = "T" Then
                        Movimiento = "Transferencia"

                    ElseIf Detalle_CE = "P" Then
                        Movimiento = "Efectivo"
                    End If

                    Cuenta_Origen = texto.Substring(88, 40)


                    Importe = " "
                    P_Aplicar = ""
                    Imp_Reg_Simplificado = ""
                    Nom_Cuenta = ""
                    Fecha_Mov = texto.Substring(178, 10)
                    Importe_Factura = ""
                    Folio_Fiscal_f = ""
                    Moneda = ""
                    Importe_cheque = texto.Substring(188, 16)
                    numero_comprobante = texto.Substring(252, 7)
                    por_ret_IVA = ""
                    por_ret_ISR = ""
                    por_ret_x = ""
                    por_ret_Y = ""
                    Tipo_Comprobante = ""
                    N = ""
                ElseIf longitud = 372 Then
                    Dia = "  "
                    Detalle_CE = texto.Substring(17, 1)
                    rfc_Ce = texto.Substring(18, 13)
                    'No_Cheque = ""
                    'No_Banco = ""
                    No_Cheque = texto.Substring(31, 3)
                    No_Banco = texto.Substring(51, 3)
                    Cuenta = "      "
                    Descripcion = "   "
                    If Detalle_CE = "P" Then
                        Movimiento = "Efectivo"
                    ElseIf Detalle_CE = "C" Then
                        Movimiento = "Factura"
                    ElseIf Detalle_CE = "H" Then
                        Movimiento = "Cheque"
                    ElseIf Detalle_CE = "T" Then
                        Movimiento = "Transferencia"
                        Banco_Des = texto.Substring(128, 3)
                        cuenta_des = texto.Substring(138, 18)
                    End If
                    'Cuenta_Origen = ""
                    Cuenta_Origen = texto.Substring(88, 40)
                    Importe = " "
                    P_Aplicar = ""
                    Imp_Reg_Simplificado = ""
                    Nom_Cuenta = ""
                    Fecha_Mov = texto.Substring(178, 10)
                    Importe_Factura = texto.Substring(188, 16)
                    Folio_Fiscal_f = texto.Substring(204, 36)
                    Moneda = texto.Substring(241, 3)
                    Importe_cheque = ""
                    numero_comprobante = texto.Substring(252, 120)
                    por_ret_IVA = ""
                    por_ret_ISR = ""
                    por_ret_x = ""
                    por_ret_Y = ""
                    Tipo_Comprobante = ""
                    N = ""
                ElseIf longitud = 261 Then ' Pagos
                    Dia = "  "
                    Detalle_CE = texto.Substring(17, 1)
                    rfc_Ce = texto.Substring(18, 13)
                    No_Cheque = texto.Substring(31, 3)
                    No_Banco = texto.Substring(51, 3)
                    Cuenta = "      "
                    Descripcion = "   "
                    Movimiento = "Efectivo"
                    Cuenta_Origen = texto.Substring(88, 40)
                    Importe = " "
                    P_Aplicar = ""
                    Imp_Reg_Simplificado = ""
                    Nom_Cuenta = ""
                    Fecha_Mov = texto.Substring(178, 10)
                    Importe_Factura = ""
                    Folio_Fiscal_f = ""
                    Moneda = ""
                    Importe_cheque = texto.Substring(188, 16)
                    numero_comprobante = texto.Substring(252, 9)
                    por_ret_IVA = ""
                    por_ret_ISR = ""
                    por_ret_x = ""
                    por_ret_Y = ""
                    Tipo_Comprobante = ""
                    N = ""

                End If

                ' Me.TablaImportar.Rows.Add(Anio, Mes, Tipo, Poliza, Dia, Detalle_CE, rfc_Ce, No_Cheque, No_Banco, Cuenta, Descripcion, Movimiento, Cuenta_Origen, Importe, P_Aplicar, Imp_Reg_Simplificado, Nom_Cuenta, Fecha_Mov, Importe_Factura, Folio_Fiscal_f, Moneda, Importe_cheque, numero_comprobante, por_ret_IVA, por_ret_ISR, por_ret_x, por_ret_Y, Tipo_Comprobante, N, texto, longitud)
                Me.TablaImportar.Rows.Add(Anio, Mes, Tipo, Poliza, Dia, Detalle_CE, rfc_Ce, No_Cheque, No_Banco, Cuenta, Descripcion, Movimiento, Cuenta_Origen, Banco_Des, cuenta_des, Importe, P_Aplicar, Imp_Reg_Simplificado, Nom_Cuenta, Fecha_Mov, Importe_Factura, Folio_Fiscal_f, Moneda, Importe_cheque, numero_comprobante, por_ret_IVA, por_ret_ISR, por_ret_x, por_ret_Y, Tipo_Comprobante, N, texto, longitud)
                If longitud = 198 Then
                    Dim valor As TipoPolizas
                    Dim Fila As DataGridViewCell
                    valor = Tp.Find(Function(S) S.Tipo = Tipo)
                    Try
                        If valor.Tipo = "" Or valor.Tipo Is Nothing Then
                            contador += 1
                            Fila = TablaImportar.Rows(Me.TablaImportar.Rows.Count - 1).Cells(2)
                            Fila.Style.ForeColor = Color.Red
                            Fila.Style.Font = New Font(TablaImportar.Font, FontStyle.Bold)
                        End If
                    Catch ex As Exception
                        contador += 1
                        Fila = TablaImportar.Rows(Me.TablaImportar.Rows.Count - 1).Cells(2)
                        Fila.Style.ForeColor = Color.Red
                        Fila.Style.Font = New Font(TablaImportar.Font, FontStyle.Bold)
                    End Try


                End If

            End If


        End While
        If contador > 0 Then
            MessageBox.Show("No se han creados algunos tipos de poliza en el sistema, antes de guardar las polizas deben darlos de alta", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
    End Sub


    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
		Leer()
		Colorear()
		Cargar_Cat(Me.lstCliente.SelectItem)
		BuscarP(Me.lstCliente.SelectItem)
	End Sub
	Private Sub BuscarP(ByVal Id_Empresa As Integer)
		Dim sql = "SELECT  Convert (BIGINT , Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + REPLACE(STR(rtrim(Polizas.Num_Pol), 6), SPACE(1), '0')) as Poliza"
		sql &= " From Polizas         INNER Join Empresa On Polizas.Id_Empresa = Empresa.Id_Empresa  "
		sql &= " INNER Join Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat "
		sql &= " WHERE    Empresa.Id_Empresa = " & Id_Empresa & " ORDER BY Poliza"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
				Dim P As New Polizas
				P.Poliza = ds.Tables(0).Rows(i)("Poliza")
				Po.Add(P)
			Next
		End If
	End Sub
	Private Sub Colorear()
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.RowCount > 0 Then


			Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
			Me.Barra.Minimum = 0
			Me.Barra.Value1 = 0
			Dim contador As Integer = 0
			For Each Fila As DataGridViewRow In TablaImportar.Rows
				If Fila.Cells(11).Value = "Titulo Poliza" Then
					Fila.DefaultCellStyle.BackColor = Color.PaleGreen
					contador = contador + 1
				End If

				If Me.Barra.Value1 = Me.Barra.Maximum Then
					Me.Barra.Minimum = 0
					Me.Cursor = Cursors.Arrow
					RadMessageBox.Show("Archivo Cargado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
					Me.Barra.Value1 = 0
				Else
					Me.Barra.Value1 += 1
				End If
			Next
			Me.lblRegistros.Text = "Total de Registros: " & contador
		End If
	End Sub

	Private Sub cmdexcel_Click(sender As Object, e As EventArgs) Handles cmdexcel.Click
		If Me.TablaImportar.RowCount > 0 Then
			If Me.TablaImportar.Columns.Count > 256 Then
				MessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub
			End If

			Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
			For col As Integer = 0 To Me.TablaImportar.Columns.Count - 1
				Eventos.EscribeExcel(excel, 1, col + 1, Me.TablaImportar.Columns(col).HeaderText)
			Next
			For i As Integer = 0 To Me.TablaImportar.RowCount - 1
				For j As Integer = 0 To Me.TablaImportar.Columns.Count - 1
					Eventos.EscribeExcel(excel, i + 2, j + 1, Valor(Me.TablaImportar.Item(j, i).Value))
				Next

			Next
			Eventos.Mostrar_Excel(excel)
		Else
			MessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
		End If
	End Sub

	Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			If Me.lstCliente.SelectText <> "" Then
				'If Me.LstTipo_Polizas.SelectText <> "" Then
				If RadMessageBox.Show("El cliente " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
					'    For Each Fila As DataGridViewRow In TablaImportar.Rows
					'    Fila.Cells(31).Value = Busca_tipificar(Me.TablaImportar.Item(tipo.Index, p).Value)
					'Next
					If Ctas.Count > 0 Then
						Hacer_Id_polizas()
					Else
						RadMessageBox.Show("No existe Catalogo de cuentas para: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
					End If
				End If
				'Else
				'    Exit Sub
				'End If
			Else
				RadMessageBox.Show("No se ha seleccionado un Cliente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			End If
		Else
			RadMessageBox.Show("No se ha Importado ningun Archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
		End If
		Me.CmdLimpiar.PerformClick()
	End Sub
    Private Sub Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                           ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                           ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
        sql &= "     ID_anio,        "
        sql &= "     ID_mes,        "
        sql &= "     ID_Dia,        "
        sql &= "     consecutivo,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "      Num_Pol,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     Id_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario   ,Aplicar_Poliza      "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & Trim(id_poliza) & "'," '@id_poliza,         
        sql &= " 	" & Trim(anio) & "," '@id_anio,           
        sql &= " 	'" & Trim(mes) & "'," '@id_mes,    
        sql &= " 	'" & Trim(dia) & "'," '@id_dia,      
        sql &= " 	" & consecutivo & "," '@consecutivo,       
        sql &= " 	" & tipo & "," '@id_tipo_poliza_sat, 
        sql &= " " & Trim(num_pol) & " ," '@num_pol, 
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha,             
        sql &= " 	'" & Trim(concepto) & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura, representa la fecha de la poliza para contabilizar    
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "',1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPoliz", sql)

        End If
    End Sub
    Private Sub Actualiza_Registro(ByVal poliza As String, ByVal uuid As Integer)
        Dim sql As String = " UPDATE dbo.xml_sat
                        SET Id_Poliza = '" & poliza & "'
                        WHERE UUID = " & uuid & "  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If

    End Sub
    Private Sub Actualiza_RegistroC(ByVal poliza As String, ByVal uuid As Integer)
        Dim sql As String = " UPDATE dbo.Xml_Complemento
                        SET Id_Poliza = '" & poliza & "'
                        WHERE IdDocumento = " & uuid & "  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If

    End Sub
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
									ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String, ByVal Descripcion As String)
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
		sql &= " No_cheque,Descripcion  "
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
		sql &= " '" & cheque & "','" & Descripcion & "'" '@no_cheque      
		sql &= " 	)"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("InsertarPolizD", sql)
		End If
	End Sub
	Private Sub Inserta_Comprobante_Fiscal(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
						   ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
						   ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Double)
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
        sql &= " 	Detalle_Comp_Electronico,Id_Empresa"
        sql &= "    )                         "
		sql &= " VALUES "
		sql &= "(                             "
		sql &= " '" & Trim(anio) & "',	" '@id_anio,                   
		sql &= " '" & Trim(mes) & "'," '@id_mes,                    
		sql &= " '" & Trim(id_poliza) & "'," '@id_poliza,                 
		sql &= " '" & Trim(Rfc_Emisor) & "'," '@rfc_emisor,                
		sql &= " '" & Trim(Folio_Fiscal) & "'," '@folio_fiscal,              
		sql &= " '" & Trim(Referencia) & "'," '@referencia,                
		sql &= " " & Trim(Importe) & "	," '@importe,                   
		sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_comprobante,         
        sql &= " '" & Trim(tipo) & "'," & Me.lstCliente.SelectItem & "" '@detalle_comp_electronico   
        sql &= " )"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("InsertarFacturas", sql)

		End If
	End Sub
	Private Sub Inserta_Comprobante_Fiscal_Cheque(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
						   ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
						   ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Double)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    id_poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & Trim(anio) & "',	" '@id_anio,                   
        sql &= " '" & Trim(mes) & "'," '@id_mes,     
        sql &= " '" & Trim(tipo) & "'," '@tipo    

        sql &= " '" & Trim(Rfc_Emisor) & "'," '@rfc_ce,                
        sql &= " '" & Trim(No_cheque) & "'," '@no_cheque,  
        sql &= " '" & Trim(no_banco) & "'," '@no_banco,  
        sql &= " '" & Trim(cuenta_origen) & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Trim(Importe) & "	," '@importe,                    
        sql &= " '" & Trim(id_poliza) & "', " '@id_poliza,  
        sql &= " 'H' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeH", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Efectivo(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Double)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(     anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    id_poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & Trim(anio) & "',	" '@id_anio,                   
        sql &= " '" & Trim(mes) & "'," '@id_mes,     
        sql &= " '" & Trim(tipo) & "'," '@tipo    

        sql &= " '" & Trim(Rfc_Emisor) & "'," '@rfc_ce,                
        sql &= " '" & Trim(No_cheque) & "'," '@no_cheque,  
        sql &= " '" & Trim(no_banco) & "'," '@no_banco,  
        sql &= " '" & Trim(cuenta_origen) & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Trim(Importe) & "	," '@importe,                    
        sql &= " '" & Trim(id_poliza) & "', " '@id_poliza,  
        sql &= " 'P' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeE", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    id_poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino	) VALUES	("

        sql &= " '" & Trim(anio) & "',	" '@id_anio,                   
        sql &= " '" & Trim(mes) & "'," '@id_mes,     
        sql &= " '" & Trim(tipo) & "'," '@tipo    

        sql &= " '" & Trim(Rfc_Emisor) & "'," '@rfc_ce,                
        sql &= " '" & Trim(No_cheque) & "'," '@no_cheque,  
        sql &= " '" & Trim(no_banco) & "'," '@no_banco,  
        sql &= " '" & Trim(cuenta_origen) & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Trim(Importe) & "	," '@importe,                    
        sql &= " '" & Trim(id_poliza) & "', " '@id_poliza,  
        sql &= " 'T' ,   '" & Trim(bancoD) & "', '" & Trim(cuentaD.Replace("/", "")) & "'   " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeT", sql)
        End If
    End Sub
    Private Function Busca_tipificar(ByVal tipos As String)
        Dim tipo As String = ""
        Dim sql As String = " SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & Me.lstCliente.SelectItem & " AND clave = '" & tipos & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tipo = ds.Tables(0).Rows(0)(0)
        Else
            tipo = 0
        End If
        Return tipo
    End Function
    Private Sub Hacer_Id_polizas()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
        Me.Barra.Minimum = 0
        Me.Barra.Value1 = 0
        For p As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Try


                If Me.TablaImportar.Item(Mov.Index, p).Value = "Titulo Poliza" Then
                    Dim Ley As New Polizas
                    Ley.Poliza = Trim(Me.TablaImportar.Item(anio.Index, p).Value & Me.TablaImportar.Item(mes.Index, p).Value & Me.TablaImportar.Item(tipo.Index, p).Value & Me.TablaImportar.Item(numpol.Index, p).Value)

                    If Not Po.Exists(Function(x) x.Poliza = Ley.Poliza) Then
                        Dim entro As String = ""
                        Dim ti As Integer

                        ti = Busca_tipificar(Me.TablaImportar.Item(tipo.Index, p).Value)

                        Dim poliza As String = Eventos.Num_poliza(Me.lstCliente.SelectItem, ti, Me.TablaImportar.Item(anio.Index, p).Value, Me.TablaImportar.Item(mes.Index, p).Value, ti)
                        Me.TablaImportar.Item(Pol.Index, p).Value = poliza

                        Dim Dto() As String = Split(poliza, "-")
                        Dim consecutivo As Integer = Dto(1)
                        Creapoliza(Me.TablaImportar.Item(Pol.Index, p).Value, Me.TablaImportar.Item(anio.Index, p).Value, Me.TablaImportar.Item(mes.Index, p).Value,' crea la poliza
                                  Me.TablaImportar.Item(dia.Index, p).Value, consecutivo,
                                  ti, Me.TablaImportar.Item(Fmov.Index, p).Value,
                                  Me.TablaImportar.Item(des.Index, p).Value, Me.TablaImportar.Item(5, p).Value, Me.TablaImportar.Item(numpol.Index, p).Value)
                        Dim contador As Integer = 0
                        For i As Integer = p To Me.TablaImportar.Rows.Count - 1
                            Dim leyendaC As String = Trim(Me.TablaImportar.Item(anio.Index, i).Value & Me.TablaImportar.Item(mes.Index, i).Value & Me.TablaImportar.Item(tipo.Index, i).Value & Me.TablaImportar.Item(numpol.Index, i).Value)

                            If Ley.Poliza = leyendaC Then 'verifica leyendas para ingresar poliza
                                'Controla el Item de la poliza
                                Me.TablaImportar.Item(Pol.Index, i).Value = poliza 'Asigna polizas a Registros pertenecientes

                                If Me.TablaImportar.Item(Mov.Index, i).Value = "Cargo" Then ' crea los Cargos con Abono 0
                                    contador = contador + 1
                                    Dim C As New Cuentas
                                    C.Cuenta = Me.TablaImportar.Item(Cta.Index, i).Value
                                    If Not Ctas.Exists(Function(x) x.Cuenta = C.Cuenta) Then
                                        Dim rf() As String = Split(Me.TablaImportar.Item(NomCta.Index, i).Value, " ")
                                        Dim r As String = rf(0)
                                        If Len(r) = 12 Or Len(r) = 13 Then
                                            If Char.IsLetter(r.Substring(0, 3)) And IsNumeric(r.Substring(4, 5)) And (IsNumeric(r.Chars(r.Length() - 1)) Or UCase(r.Chars(r.Length() - 1)) = "A") Then
                                                r = r
                                            Else
                                                r = ""
                                            End If
                                        Else
                                            r = ""
                                        End If
                                        Crear_cuenta(Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(0, 4), Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(4, 4), Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(8, 4),
                                    Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(12, 4), Me.TablaImportar.Item(Cta.Index, i).Value, Me.TablaImportar.Item(NomCta.Index, i).Value, Me.lstCliente.SelectItem, "", r)

                                    End If

                                    Crea_detalle_poliza(Me.TablaImportar.Item(Pol.Index, i).Value, contador, Convert.ToDecimal(Me.TablaImportar.Item(imp.Index, i).Value),
                                        0, Me.TablaImportar.Item(Cta.Index, i).Value, "", Me.TablaImportar.Item(des.Index, i).Value)

                                ElseIf Me.TablaImportar.Item(Mov.Index, i).Value = "Abono" Then ' Crea los Abonos con Cargo 0
                                    contador = contador + 1

                                    Dim C As New Cuentas
                                    C.Cuenta = Me.TablaImportar.Item(Cta.Index, i).Value
                                    If Not Ctas.Exists(Function(x) x.Cuenta = C.Cuenta) Then
                                        Dim rf() As String = Split(Me.TablaImportar.Item(NomCta.Index, i).Value, " ")
                                        Dim r As String = rf(0)
                                        If Len(r) = 12 Or Len(r) = 13 Then
                                            If Char.IsLetter(r.Substring(0, 3)) And IsNumeric(r.Substring(4, 5)) And (IsNumeric(r.Chars(r.Length() - 1)) Or UCase(r.Chars(r.Length() - 1)) = "A") Then
                                                r = r
                                            Else
                                                r = ""
                                            End If
                                        Else
                                            r = ""
                                        End If
                                        Crear_cuenta(Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(0, 4), Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(4, 4), Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(8, 4),
                                    Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(12, 4), Me.TablaImportar.Item(Cta.Index, i).Value, Me.TablaImportar.Item(NomCta.Index, i).Value, Me.lstCliente.SelectItem, "", r)

                                    End If

                                    Crea_detalle_poliza(Me.TablaImportar.Item(Pol.Index, i).Value, contador, 0,
                                       Convert.ToDecimal(Me.TablaImportar.Item(imp.Index, i).Value), Me.TablaImportar.Item(Cta.Index, i).Value, "", Me.TablaImportar.Item(des.Index, i).Value)

                                ElseIf Me.TablaImportar.Item(Mov.Index, i).Value = "Factura" Then ' se agregan las facturas 
                                    If Buscafactura(Me.TablaImportar.Item(ff.Index, i).Value, Me.TablaImportar.Item(DCE.Index, i).Value, poliza) = True Then
                                        Inserta_Comprobante_Fiscal(Me.TablaImportar.Item(Pol.Index, i).Value, Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value,
                                                           Me.TablaImportar.Item(RFCCE.Index, i).Value, Me.TablaImportar.Item(DCE.Index, i).Value, Me.TablaImportar.Item(Fmov.Index, i).Value,
                                                           Me.TablaImportar.Item(ff.Index, i).Value, "Factura " & Me.TablaImportar.Item(RFCCE.Index, i).Value & " " & Me.TablaImportar.Item(DCE.Index, i).Value, Me.TablaImportar.Item(ImpFactura.Index, i).Value)
                                    End If
                                ElseIf Me.TablaImportar.Item(Mov.Index, i).Value = "Cheque" Then ' 'Insertar cheque
                                    If BuscaCE(Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(RFCCE.Index, i).Value,
                                         Me.TablaImportar.Item(Fmov.Index, i).Value, Val(Me.TablaImportar.Item(ImpFactura.Index, i).Value), "T") = True Then
                                        Inserta_Comprobante_Fiscal_Cheque(Me.TablaImportar.Item(Pol.Index, i).Value, Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value,
                             Me.TablaImportar.Item(RFCCE.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(Fmov.Index, i).Value,
                           Me.TablaImportar.Item(NCheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(CtaO.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, IIf(Me.TablaImportar.Item(ImpChe.Index, i).Value = "", 0, Me.TablaImportar.Item(ImpChe.Index, i).Value))
                                        'Me.TablaImportar.Item(ncheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(ctao.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, Me.TablaImportar.Item(imp.Index, i).Value)
                                    End If
                                ElseIf Me.TablaImportar.Item(Mov.Index, i).Value = "Transferencia" Then  ' Insertar Transferencia 

                                    If BuscaCE(Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(RFCCE.Index, i).Value,
                                         Me.TablaImportar.Item(Fmov.Index, i).Value, Val(Me.TablaImportar.Item(ImpFactura.Index, i).Value), "T") = True Then
                                        Inserta_Comprobante_Fiscal_Transf(Me.TablaImportar.Item(Pol.Index, i).Value, Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value,
                            Me.TablaImportar.Item(RFCCE.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(Fmov.Index, i).Value,
                            Me.TablaImportar.Item(NCheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(CtaO.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, IIf(Me.TablaImportar.Item(ImpChe.Index, i).Value = "", 0, Me.TablaImportar.Item(ImpChe.Index, i).Value), Me.TablaImportar.Item(BancoD.Index, i).Value, Me.TablaImportar.Item(CtaD.Index, i).Value)
                                        'Me.TablaImportar.Item(ncheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(ctao.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, Me.TablaImportar.Item(imp.Index, i).Value)
                                    End If
                                ElseIf Me.TablaImportar.Item(Mov.Index, i).Value = "Efectivo" Then  ' Insertar Efectivo 

                                    If BuscaCE(Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(RFCCE.Index, i).Value,
                                         Me.TablaImportar.Item(Fmov.Index, i).Value, Val(Me.TablaImportar.Item(ImpFactura.Index, i).Value), "P") = True Then
                                        Inserta_Comprobante_Fiscal_Efectivo(Me.TablaImportar.Item(Pol.Index, i).Value, Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(mes.Index, i).Value,
                                          Me.TablaImportar.Item(RFCCE.Index, i).Value, Me.TablaImportar.Item(tipo.Index, p).Value, Me.TablaImportar.Item(Fmov.Index, i).Value,
                                          Me.TablaImportar.Item(NCheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(CtaO.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, IIf(Me.TablaImportar.Item(ImpChe.Index, i).Value = "", 0, Me.TablaImportar.Item(ImpChe.Index, i).Value))
                                        'Me.TablaImportar.Item(ncheque.Index, i).Value, Me.TablaImportar.Item(Banco.Index, i).Value, Me.TablaImportar.Item(ctao.Index, i).Value, Me.TablaImportar.Item(des.Index, i).Value, Me.TablaImportar.Item(imp.Index, i).Value)
                                    End If
                                End If
                            Else
                                Exit For
                            End If
                        Next
                    Else
                        'Mensaje de poliza Existente


                    End If




                End If






                'If Busca_Poliza(Me.TablaImportar.Item(anio.Index, p).Value, Me.TablaImportar.Item(mes.Index, p).Value, Val(Me.TablaImportar.Item(dia.Index, p).Value), Me.TablaImportar.Item(numpol.Index, p).Value, IIf(Busca_tipificar(Me.TablaImportar.Item(tipo.Index, p).Value) = "N/A", 0, Busca_tipificar(Me.TablaImportar.Item(tipo.Index, p).Value)), Me.lstCliente.SelectItem) = True Then
                '    Dim leyenda As String = Trim(Me.TablaImportar.Item(anio.Index, p).Value & Me.TablaImportar.Item(mes.Index, p).Value & Me.TablaImportar.Item(tipo.Index, p).Value & Me.TablaImportar.Item(numpol.Index, p).Value)







            Catch ex As Exception

            End Try
            If Me.Barra.Value1 = Me.Barra.Maximum Then
                Me.Barra.Minimum = 0
                Me.Cursor = Cursors.Arrow
                RadMessageBox.Show("Pólizas Importadas", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Barra.Value1 = 0
            Else
                Me.Barra.Value1 += 1
            End If
        Next
        Me.CmdLimpiar.PerformClick()
    End Sub
    Private Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String, ByVal Poliza As String)
        Dim hacer As Boolean
        Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            Dim id As Integer = 0
            Try
                id = Convert.ToInt32(Eventos.ObtenerValorDB("Xml_Complemento", "Id_Xml_Complemento", " IdDocumento ='" & Folio_Fiscal & "'", True))
            Catch ex As Exception
                id = 0
            End Try

            If id <> 0 Then
                Actualiza_RegistroC(Poliza, Folio_Fiscal)
                Dim query As String = "SELECT Xml_Sat.Id_Registro_Xml  FROM Xml_Sat WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND UUID = '" & Folio_Fiscal & "' "
                Dim data As DataSet = Eventos.Obtener_DS(query)
                If data.Tables(0).Rows.Count > 0 Then
                    Eventos.Actualiza_Factura(Folio_Fiscal)
                End If
            Else
                Actualiza_Registro(Poliza, Folio_Fiscal)
            End If


            hacer = True
        End If
        Return hacer
    End Function


    Private Function BuscaCE(ByVal anio As String, ByVal mes As String, ByVal tipo As String, ByVal RFC_Ce As String,
                             ByVal Fecha_Mov As String, ByVal imp As Double, ByVal Tipo_CE As String)
        Dim hacer As Boolean
        Dim sql As String = "SELECT * FROM dbo.Conta_E_Sistema
                                 WHERE Anio = '" & Trim(anio) & "' AND Mes = '" & Trim(mes) & "' AND Tipo = '" & Trim(tipo) & "' AND RFC_Ce = '" & Trim(RFC_Ce) & "' 
                                 AND Fecha_Mov = " & Eventos.Sql_hoy(Fecha_Mov) & " AND Importe = " & imp & "  AND Tipo_CE = '" & Trim(Tipo_CE) & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Function Buscarcuenta(ByVal cliente As Integer, ByVal cuenta As String)
		Dim hacer As Boolean
		Dim sql As String = " select * from catalogo_de_cuentas where Id_Empresa = " & cliente & " and cuenta =" & cuenta & ""
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = False
		Else
			hacer = True
		End If
		Return hacer
	End Function
	Private Sub Cargar_Cat(ByVal cliente As Integer)
		Dim sql As String = " Select Cuenta from catalogo_de_cuentas where Id_Empresa = " & cliente & "  "
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
				Dim C As New Cuentas
				C.Cuenta = ds.Tables(0).Rows(i)("Cuenta")
				Ctas.Add(C)
			Next
		End If
	End Sub
	Public Class Cuentas
		Property Cuenta As String
	End Class
	Public Class Polizas
		Property Poliza As String
	End Class
    Public Class TipoPolizas
        Property Tipo As String
    End Class
    Private Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
							 ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, ByVal letra As String, ByVal RFC As String)
		Dim ds As DataSet = Eventos.Obtener_DS("select Naturaleza, clasificacion,clave,Balanza,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and Id_Empresa = " & cliente & "  ")

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
			sql &= "Id_Empresa,clave,  "

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
			sql &= "	" & cliente & " , '" & Trim(letra) & "'," '@Id_Empresa     
			sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Balanza")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Balanza"))) & "," '@Balanza
			sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_ceros")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_ceros"))) & "," '@Cta_ceros    
			sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Cargo_Cero")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_Cargo_Cero"))) & "," '@Cta_Cargo_Cero
			sql &= "	" & IIf(IsDBNull(ds.Tables(0).Rows(0)("Cta_Abono_Cero")) = True, 0, Eventos.Bool2(ds.Tables(0).Rows(0)("Cta_Abono_Cero"))) & "" '@Cta_Abono_Cero  
			sql &= "  )"
			' Ingresar codigo para importar catalogos
			If Eventos.Comando_sql(sql) > 0 Then
				Dim C As New Cuentas
				C.Cuenta = cuenta
				Ctas.Add(C)
				Eventos.Insertar_usuariol("Importa_catalogo_Pol", sql)
			End If
		Else

		End If

	End Sub
	Private Function Buscar_movimiento(ByVal factura As String)
		Dim polizas As String = ""
		Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & factura & "'  "
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			polizas = ds.Tables(0).Rows(0)("Id_Poliza")
		Else
			polizas = ""
		End If
		Return polizas
	End Function
	Private Function Verifica_catalogo_cliente(ByVal cliente As Integer)
		Dim hacer As Boolean
		Dim sql As String = " Select * From catalogo_de_cuentas where Id_Empresa = " & cliente & ""
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			hacer = True
		Else
			hacer = False
		End If
		Return hacer
	End Function
	Private Function Checa_tipo(ByVal tipo As String, ByVal cliente As Integer)
		Dim clave As String = ""
		Dim sql As String = "SELECT id_tipo_poliza FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & cliente & " AND clave = '" & tipo & "'"
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			clave = ds.Tables(0).Rows(0)("id_tipo_poliza")
		Else
			clave = 0
		End If
		Return clave

	End Function

	Private Function Verificapoliza()
		Dim Hacer As Boolean
		Dim sql As String = ""
		Dim ds As DataSet = Eventos.Obtener_DS(sql)
		If ds.Tables(0).Rows.Count > 0 Then
			Hacer = False
		Else
			Hacer = True
		End If
		Return Hacer
	End Function


End Class