Imports Telerik.WinControls
Public Class Poliza_Modelo
    Dim Activo As Boolean
    Dim mes = Now.Date.Month.ToString
    Dim anio = Str(DateTime.Now.Year)
    Public Nuevo As Boolean
    Private Sub Poliza_Modelo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Cargar_Listas()
        Limpiar()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2004 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Items.Add("*")
        Me.comboAño.Text = anio
        Me.ComboMes.Items.Add("*")
        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Me.ComboMes.Text = mes
        Ventanas(False, False, True)
        Me.TablaDetalle.BackColor = Color.LightSteelBlue
        Me.TablaDetalle.BorderStyle = BorderStyle.Fixed3D
        Me.TablaDetalle.RowsDefaultCellStyle.BackColor = Color.LightSteelBlue
        Me.TablaDetalle.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Bold)
        Me.TablaDetalle.RowsDefaultCellStyle.SelectionBackColor = Color.Tomato
        Me.TablaDetalle.GridColor = Color.SteelBlue
        Me.TablaDetalle.Dock = Dock.Fill
        Me.TablaDetalle.EnableHeadersVisualStyles = False
        Me.TablaDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        Me.TablaDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke
        Me.TablaDetalle.ColumnHeadersDefaultCellStyle.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Bold)
        Me.TablaDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.TablaDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.TablaDetalle.ColumnHeadersHeight = 30
        Me.TablaDetalle.RowHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue
        Me.TablaDetalle.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Tomato
        Me.TablaDetalle.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        Me.TablaDetalle.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        Me.TablaDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.TablaDetalle.AllowUserToAddRows = False
        Me.TablaDetalle.ScrollBars = ScrollBars.Both
        Activo = False
    End Sub
    Private Sub Ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.Cmdguardar.Enabled = True
            Me.CmdNuevo.Enabled = False

        ElseIf load = True Then
            Me.Cmdguardar.Enabled = False
            Me.CmdNuevo.Enabled = True
        Else
            Me.Cmdguardar.Enabled = False
            Me.CmdNuevo.Enabled = False
        End If
    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Me.LstLetras.Cargar("Select 'GG' as Clave , 'GG' as cL union select 'PARAMETRO' as Clave , 'PARAMETRO' as cL  ")
        Me.LstLetras.SelectText = ""
        Me.LstPolClientes.Cargar(" Select  Id_Tipo_Pol_Sat, clave +'-'+ Nombre as Nombre from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
        Me.LstPolClientes.SelectText = ""
    End Sub
    Private Sub Limpiar()
        Me.txtfechaPol.Text = ""
        Me.txtconcepto.Text = ""
        If Me.TablaDetalle.Rows.Count > 0 Then
            Me.TablaDetalle.Rows.Clear()
        Else
        End If
        Me.LstPolClientes.SelectText = ""
        Me.TxtNumpol.Text = ""
        Me.txtabono.Text = ""
        Me.txtcargo.Text = ""

    End Sub
    Private Sub CreapolizaS(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String, ByVal numpol As Integer,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,"
        sql &= "     ID_anio,"
        sql &= "     ID_mes, "
        sql &= "     ID_dia,        "
        sql &= "     Num_Pol,    "
        sql &= "     consecutivo,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     Id_Empresa,     "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario ,Aplicar_Poliza          "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,      
        sql &= " 	'" & dia & "'," '@id_dia,  
        sql &= " 	'" & numpol & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,       
        sql &= " 	" & Trim(tipo) & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@id_cliente,        

        sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(My.Forms.Inicio.LblUsuario.Text) & "',1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then

            If Me.TablaDetalle.Rows.Count > 0 Then
                For i As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                    If Me.TablaDetalle.Item(cta.Index, i).Value <> "" And Me.TablaDetalle.Item(cta.Index, i).Value <> Nothing Then
                        If Me.TablaDetalle.Item(Abon.Index, i).Value <> 0 Or Me.TablaDetalle.Item(Car.Index, i).Value <> Nothing Then
                            Crea_detalle_polizaS(id_poliza, i + 1, Me.TablaDetalle.Item(Car.Index, i).Value,
                                       Me.TablaDetalle.Item(Abon.Index, i).Value, Me.TablaDetalle.Item(cta.Index, i).Value)
                        End If

                    End If
                Next
                Me.CmdLimpiar.PerformClick()
            End If
        End If
    End Sub
    Private Sub Crea_detalle_polizaS(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                    ByVal Abono As Decimal, ByVal cuenta As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
        sql &= " ID_item,       "
        sql &= " Cargo,          "
        sql &= " Abono,         "
        sql &= " Fecha_captura,  "
        sql &= " Movto,"
        sql &= " Cuenta "
        sql &= " ) "
        sql &= " VALUES "
        sql &= "( "
        sql &= " '" & id_poliza & "'	," '@id_poliza,     
        sql &= "" & item & "," '@id_item,       
        sql &= "" & cargo & "," '@cargo,         
        sql &= "" & Abono & "," '@abono,         
        sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
        sql &= " 'A'	," '@movto,         
        sql &= " " & cuenta & "	" '@cuenta,        
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Function Calcula_poliza()
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Me.LstPolClientes.SelectItem, Trim(Me.comboAño.Text), Trim(Me.ComboMes.Text), Me.LstPolClientes.SelectItem)
        Return poliza
    End Function
    Private Sub TablaDetalle_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles TablaDetalle.CellValueChanged
        If Activo = False Then
            Dim col As Integer = 0
            If Me.TablaDetalle.Rows.Count > 0 Then
                Calcula_total()
                ' Clonacion Valores
                Automaticos()
            End If
        End If
    End Sub
    Private Sub Automaticos()
        If RadProvIMSS.Checked = True Then
            If Me.TablaDetalle.Item(0, Me.TablaDetalle.CurrentRow.Index).Value = "6015001000260001" Then
                For I As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                    If Me.TablaDetalle.Item(0, I).Value = "2110000100010000" Then
                        Me.TablaDetalle.Item(3, I).Value = TablaDetalle.Item(2, Me.TablaDetalle.CurrentRow.Index).Value * 1
                    End If
                Next
            ElseIf Me.TablaDetalle.Item(0, Me.TablaDetalle.CurrentRow.Index).Value = "6015001000280000" Then
                For I As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                    If Me.TablaDetalle.Item(0, I).Value = "2110000200000000" Then
                        Me.TablaDetalle.Item(3, I).Value = TablaDetalle.Item(2, Me.TablaDetalle.CurrentRow.Index).Value * 1
                    End If
                Next
            ElseIf Me.TablaDetalle.Item(0, Me.TablaDetalle.CurrentRow.Index).Value = "6015001000260002" Then
                For I As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                    If Me.TablaDetalle.Item(0, I).Value = "2110000100020000" Then
                        Me.TablaDetalle.Item(3, I).Value = TablaDetalle.Item(2, Me.TablaDetalle.CurrentRow.Index).Value * 1
                    End If
                Next
            ElseIf Me.TablaDetalle.Item(0, Me.TablaDetalle.CurrentRow.Index).Value = "6015001000270000" Then
                For I As Integer = 0 To Me.TablaDetalle.Rows.Count - 1
                    If Me.TablaDetalle.Item(0, I).Value = "2110000300000000" Then
                        Me.TablaDetalle.Item(3, I).Value = TablaDetalle.Item(2, Me.TablaDetalle.CurrentRow.Index).Value * 1
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Calcula_total()
        Dim tcargo As Decimal = 0
        Dim tabono As Decimal = 0
        Dim tparcial As Decimal = 0
        For i As Integer = 0 To Me.TablaDetalle.RowCount - 1
            If Me.TablaDetalle.Item(Car.Index, i).Value = Nothing Then
                Me.TablaDetalle.Item(Car.Index, i).Value = 0
            End If
            If Me.TablaDetalle.Item(Abon.Index, i).Value = Nothing Then
                Me.TablaDetalle.Item(Abon.Index, i).Value = 0
            End If
            tcargo = tcargo + Me.TablaDetalle.Item(Car.Index, i).Value
            tabono = tabono + Me.TablaDetalle.Item(Abon.Index, i).Value

        Next
        Me.txtcargo.Text = Format(tcargo, "#,###.#0")
        Me.txtabono.Text = Format(tabono, "#,###.#0")
        If Me.txtcargo.Text <> Me.txtabono.Text Then
            Me.Label3.Text = "La Póliza no cuadra"
            Me.txtcargo.BackColor = Color.Red
            Me.txtabono.BackColor = Color.Red
            Me.Label3.ForeColor = Color.Red
            Me.Cmdguardar.Enabled = False
        Else
            Me.Label3.Text = "Totales:"
            Me.txtcargo.BackColor = Color.White
            Me.txtabono.BackColor = Color.White
            Me.Cmdguardar.Enabled = True
        End If


    End Sub
    Private Sub CmdNuevo_Click(sender As Object, e As EventArgs) Handles CmdNuevo.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            Nuevo = True
            Activo = True

            Ventanas(True, False, False)
            ' If Me.LstLetras.SelectText <> "" Then
            Crea_Registros()
            'End If
            Activo = False
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa .", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            Exit Sub
        End If
    End Sub
    Private Sub Cmdguardar_Click(sender As Object, e As EventArgs) Handles Cmdguardar.Click
        If Nuevo = True Then
            Dim poliza As String = Calcula_poliza()
            Dim posicion As Integer = InStr(1, poliza, "-", CompareMethod.Binary)
            Dim cuantos As Integer = Len(poliza) - Len(poliza.Substring(0, posicion))
            Dim consecutivo As Integer = Val(poliza.Substring(posicion, cuantos))
            'verificar dia
            If Me.TablaDetalle.Rows.Count > 0 Then
                CreapolizaS(poliza, Me.txtfechaPol.Text.ToString.Substring(6, 4), Me.txtfechaPol.Text.ToString.Substring(3, 2), Me.txtfechaPol.Text.ToString.Substring(0, 2), Me.TxtNumpol.Text,
                          consecutivo, Me.LstPolClientes.SelectItem, Me.txtfechaPol.Text,
                          Trim(Me.txtconcepto.Text))
            End If
        End If
    End Sub
    Private Sub lstCliente_cambio_item(value As String, texto As String) Handles lstCliente.Cambio_item
        If Activo = False Then
            Activo = True
            Me.LstPolClientes.Cargar(" Select  Id_Tipo_Pol_Sat, clave +'-'+ Nombre as Nombre from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            Me.LstPolClientes.SelectText = ""
            Activo = False
        End If
    End Sub
    Private Sub Busca_Parametros(ByVal empresa As Integer)

    End Sub
    Private Sub Crea_Registros()
        Dim leyenda As String = ""
        Dim ds As DataSet = Eventos.Obtener_DS("Select clave from Letras_Contabilidad where Id_Empresa = " & Me.lstCliente.SelectItem & " AND clave IN('GV','GA','GF','GG')")
        Dim posi As Integer = 0
        Dim le As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            If RadProvIMSS.Checked = True Then
                Me.txtconcepto.Text = "Provision IMSS del mes de " & Eventos.MesEnletra(Me.ComboMes.Text)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    le = Trim(ds.Tables(0).Rows(i)("clave"))
                    Select Case le
                        Case "GG"

                            Me.TablaDetalle.Rows.Insert(posi, "6015001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                        Case "GA"

                            Me.TablaDetalle.Rows.Insert(posi, "6035001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                        Case "GF"

                            Me.TablaDetalle.Rows.Insert(posi, "6045001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                        Case "GV"

                            Me.TablaDetalle.Rows.Insert(posi, "6025001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                    End Select
                Next

                Me.TablaDetalle.Rows.Insert(posi, "2110000100010000", "Prov IMSS Patronal", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000100020000", "Prov Cesantia Y vejez Patronal", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000200000000", "Provision de SAR por pagar", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000300000000", "Provis de infonavit por pagar", 0, 0)

            ElseIf RadPagoProv.Checked = True Then
                Me.txtconcepto.Text = "Pago IMSS del mes de " & Eventos.MesEnletra(Me.ComboMes.Text)
                Me.TablaDetalle.Rows.Insert(posi, "2110000100010000", "Prov IMSS Patronal", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000200000000", "Provision de SAR por pagar", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000100020000", "Prov Cesantia Y vejez Patronal", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2110000300000000", "Provis de infonavit por pagar", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2160001100010000", "IMSS cuota obrera", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "2160001200010000", "Retencion Credito Infonavit", 0, 0)
                posi = posi + 1
                Me.TablaDetalle.Rows.Insert(posi, "", "Banco", 0, 0)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    le = Trim(ds.Tables(0).Rows(i)("clave"))
                    Select Case le
                        Case "GG"
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6010001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6010001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6010001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6010001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6015001000260002", "Cesantia y Vejez", 0, 0)

                        Case "GA"
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6030001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6030001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6030001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6030001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6035001000260002", "Cesantia y Vejez", 0, 0)

                        Case "GF"
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6040001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6040001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6040001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6040001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6045001000260002", "Cesantia y Vejez", 0, 0)
                        Case "GV"
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6020001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6020001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6020001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6020001000260002", "Cesantia y Vejez", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000260001", "Cuotas al IMSS Patronal", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000280000", "Aportaciones al SAR", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000270000", "Aportaciones al infonavit", 0, 0)
                            posi = posi + 1
                            Me.TablaDetalle.Rows.Insert(posi, "6025001000260002", "Cesantia y Vejez", 0, 0)
                    End Select
                Next

            ElseIf RadProvISR.Checked = True Then

                Me.txtconcepto.Text = "Provision ISR del mes de " & Eventos.MesEnletra(Me.ComboMes.Text)
                Me.TablaDetalle.Rows.Insert(0, "1140000100000000", "Pagos Prov de ISR", 0, 0)
                Me.TablaDetalle.Rows.Insert(1, "2130000300010000", "ISR por pagar Provisional", 0, 0)

            ElseIf RadPagoImpuestos.Checked = True Then
                Me.txtconcepto.Text = "Pago de Impuestos del mes de " & Eventos.MesEnletra(Me.ComboMes.Text)
                Me.TablaDetalle.Rows.Insert(0, "2130000300010000", "ISR por pagar Provisional", 0, 0)
                Me.TablaDetalle.Rows.Insert(1, "2130000100000000", "Iva Por Pagar", 0, 0)
                Me.TablaDetalle.Rows.Insert(2, "2160000100000000", "Imp Ret por Sueldos y Salarios", 0, 0)
                Me.TablaDetalle.Rows.Insert(3, "2160000200000000", "Imp Ret por Asimilables", 0, 0)
                Me.TablaDetalle.Rows.Insert(4, "2160000300000000", "Imp Ret ISR Arrendamiento", 0, 0)
                Me.TablaDetalle.Rows.Insert(5, "2160000400000000", "Imp Ret ISR Servicios Pro", 0, 0)
                Me.TablaDetalle.Rows.Insert(6, "2160001000010000", "Ret de IVA por Honorarios", 0, 0)
                Me.TablaDetalle.Rows.Insert(7, "2160001000020000", "Ret de IVA por Arrendamiento", 0, 0)
                Me.TablaDetalle.Rows.Insert(8, "2160001000030000", "Ret de IVA por Fletes", 0, 0)
                Me.TablaDetalle.Rows.Insert(9, "6120000100010000", "Actualizacion", 0, 0)
                ds = Eventos.Obtener_DS("Select clave from Letras_Contabilidad where id_cliente = " & Me.lstCliente.SelectItem & " AND clave IN( 'GA' ,'GG')")
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    le = Trim(ds.Tables(0).Rows(i)("clave"))
                    Select Case le
                        Case "GG"
                            Me.TablaDetalle.Rows.Insert(10, "6010001000590000", "Recargos", 0, 0)
                        Case "GA"
                            Me.TablaDetalle.Rows.Insert(10, "6030001000590000", "Recargos", 0, 0)
                    End Select
                Next
                Me.TablaDetalle.Rows.Insert(11, "", "Banco", 0, 0)
                Me.TablaDetalle.Rows.Insert(12, "", "Caja Chica", 0, 0)

            End If



        End If
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Activo = True
        Limpiar()
        Ventanas(False, False, True)
        Activo = False

    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Poliza Modelo")
    'End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub


End Class
