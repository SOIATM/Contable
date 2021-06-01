Imports Telerik.WinControls

Public Class ImpuestosPM

    Dim Anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim Negrita_Amarilla As New DataGridViewCellStyle
    Dim Tasa As Decimal = 0.16
    Dim PM As Integer = 0
    Dim Datos As DataSet
    Dim Plantilla As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Public USR As String = My.Forms.Inicio.LblUsuario.Text.Trim
    Private Sub Diseño()
        Eventos.DiseñoTablaEnca(TablaErrores)
        Eventos.DiseñoTablaEnca(TablaPM)
    End Sub
    Private Sub ImpuestosPM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Cargar_Listas()
        Diseño()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Text = Anio

        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes.Text = m
        If Len(m) < 2 Then
            m = "0" & m
        End If
        Me.ComboMes2.Text = m

        Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa  = " & Me.lstCliente.SelectItem & "", True)
        If Len(RFC) = 12 Then
            'Moral
            PM = 1
        Else
            'Fisica
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa moral para usar este Componente... ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

        End If
        If PM = 0 Then
            Me.RadPanel1.Enabled = False
        End If
    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1

        Me.LstEstados.Cargar("SELECT Id_Estado, Nombre_Estado FROM Estados ORDER BY Nombre_Estado ")
        Me.LstEstados.SelectItem = 13

    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.LstEstados.SelectText <> "" Then
                If Trim(Me.comboAño.Text.Trim()) <> "" Then
                    Try
                        Me.TablaPM.Rows.Clear()
                    Catch ex As Exception

                    End Try
                    If Me.TablaPM.Rows.Count > 0 Then

                        Crear_Filas(Trim(Me.comboAño.Text.Trim()), Me.lstCliente.SelectItem)
                    Else
                        Crear_Filas(Trim(Me.comboAño.Text.Trim()), Me.lstCliente.SelectItem)
                    End If

                    Try
                        If Datos.Tables(0).Rows.Count > 0 Then
                            Cargar(Datos)
                        End If
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To Me.TablaPM.RowCount - 1

                    Next

                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")

                    RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    Exit Sub
                End If
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Debes seleccionar un Estado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Exit Sub
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
    End Sub
    Private Sub PintaTabla(ByVal Fila As Integer, ByVal Tabla As DataGridView, ByVal T As Integer)
        Negrita_verde.Font = New Font(Tabla.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_verde.Alignment = DataGridViewContentAlignment.MiddleCenter

        Negrita_morado.Font = New Font(Tabla.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Plum

        Negrita_Amarilla.Font = New Font(Tabla.Font, FontStyle.Bold)
        Negrita_Amarilla.BackColor = Color.Yellow

        Dim Azul As New DataGridViewCellStyle
        Azul.Font = New Font(Tabla.Font, FontStyle.Bold)
        Azul.BackColor = Color.CornflowerBlue

        Dim Amarilla As New DataGridViewCellStyle
        Amarilla.Font = New Font(Tabla.Font, FontStyle.Regular)
        Amarilla.BackColor = Color.Yellow

        Dim Naranja As New DataGridViewCellStyle
        Naranja.Font = New Font(Tabla.Font, FontStyle.Regular)
        Naranja.BackColor = Color.Orange

        Dim F As DataGridViewRow
        F = Tabla.Rows(Fila)
        If T = 1 Then
            F.DefaultCellStyle = Negrita_verde
        ElseIf T = 2 Then
            F.DefaultCellStyle = Negrita_morado
        ElseIf T = 3 Then
            F.DefaultCellStyle = Negrita_Amarilla
            For C As Integer = 3 To 15
                Dim S As DataGridViewCell
                S = TablaPM.Rows(Fila).Cells(C)
                S.Style.Format = "N4"
                S.ReadOnly = False
            Next
        ElseIf T = 4 Then
            F.DefaultCellStyle = Azul
            'For C As Integer = 3 To 15
            '    Dim S As DataGridViewCell
            '    S = TablaPM.Rows(Fila).Cells(C)
            '    S.Style.Format = "N2"
            '    S.ReadOnly = False
            'Next
        ElseIf T = 5 Then
            F.DefaultCellStyle = Amarilla
            'For C As Integer = 3 To 15
            '    Dim S As DataGridViewCell
            '    S = TablaPM.Rows(Fila).Cells(C)
            '    S.Style.Format = "N2"
            '    S.ReadOnly = False
            'Next
        ElseIf T = 6 Then
            F.DefaultCellStyle = Naranja
            'For C As Integer = 3 To 15
            '    Dim S As DataGridViewCell
            '    S = TablaPM.Rows(Fila).Cells(C)
            '    S.ReadOnly = False
            'Next
        End If

    End Sub
    Private Sub Crear_Filas(ByVal Anio As Integer, ByVal Cliente As Integer)
        Try
            Datos.Clear()
        Catch ex As Exception

        End Try
        Try
            Plantilla.Clear()
        Catch ex As Exception

        End Try
        Dim sql As String = " SELECT Descripcion, Cuenta, Naturaleza, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre,Anual, Anio,Suma, Operacion,Tipo
                               FROM     ImpuestosPMPF 
                               WHERE  Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Anio & " and Id_Estado = " & Me.LstEstados.SelectItem & " ORDER BY Id_Impuestos  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then



            Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " order by Alias ")
            If Cuentas.Tables(0).Rows.Count > 0 Then
                If Me.Cta.Items.Count = 0 Then
                    'For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                    '    Me.Cta.DataSource = Cuentas.Tables(0)

                    'Next
                    Me.Cta.DataSource = Cuentas.Tables(0)
                    Me.Cta.DisplayMember = Cuentas.Tables(0).Columns(0).Caption.ToString()
                End If
            End If
            Me.TablaPM.RowCount = ds.Tables(0).Rows.Count
            Datos = ds
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("No existe plantilla de la Empresa " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas crearla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Eventos.Abrir_form(CatalogoSumas)

            Else
                Exit Sub
            End If
        End If


    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        Select Case Len(valor)
            Case 4
                valor = valor & "-0000-0000-0000"
            Case 9
                valor = valor & "-0000-0000"
            Case 14
                valor = valor & "-0000"
            Case 20
                valor = valor
        End Select
        Return valor
    End Function

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        Me.TablaPM.Rows.Insert(Me.TablaPM.CurrentRow.Index + 1, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Me.TablaPM.Item(Suma.Index, Me.TablaPM.CurrentRow.Index).Value.ToString, Me.TablaPM.Item(Operacion.Index, Me.TablaPM.CurrentRow.Index).Value.ToString, Me.TablaPM.Item(Tipo.Index, Me.TablaPM.CurrentRow.Index).Value.ToString)

    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Me.lstCliente.SelectText <> "" Then
            If RadMessageBox.Show("Se actualizara la informacion del Cliente  " & Me.lstCliente.SelectText & " para el año " & Me.comboAño.Text.Trim() & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Sql As String = "Delete From ImpuestosPMPF where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.comboAño.Text) & " and Id_Estado =" & Me.LstEstados.SelectItem & " "
                If Eventos.Comando_sql(Sql) > 0 Then
                    Decodificar(2)
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub Decodificar(ByVal Tos As Integer)
        For i As Integer = 66 To 70
            For j As Integer = 3 To 13
                Try

                    If Trim(Me.TablaPM.Item(Suma.Index, i).Value) = "37" And Tos = 1 Then
                        If Me.TablaPM.Item(j, i).Value.ToString.Substring(0, 1) IsNot Nothing Then


                            Select Case Trim(Me.TablaPM.Item(j, i).Value.ToString.Substring(0, 1))
                                Case "0"
                                    Me.TablaPM.Item(j, i).Value = "A Favor"
                                Case "1"
                                    Me.TablaPM.Item(j, i).Value = "A Pagar"
                                Case "2"
                                    Me.TablaPM.Item(j, i).Value = "Acreditamiento"
                            End Select
                        End If
                    ElseIf Trim(Me.TablaPM.Item(Suma.Index, i).Value) = "37" And Tos = 2 Then
                        If Me.TablaPM.Item(j, i).Value.ToString.Substring(0, 1) IsNot Nothing Then


                            Select Case Trim(Me.TablaPM.Item(j, i).Value.ToString)
                                Case "A Favor"
                                    Me.TablaPM.Item(j, i).Value = 0
                                Case "A Pagar"
                                    Me.TablaPM.Item(j, i).Value = 1
                                Case "Acreditamiento"
                                    Me.TablaPM.Item(j, i).Value = 2
                                Case "0"
                                    Me.TablaPM.Item(j, i).Value = 0
                            End Select
                        End If
                    End If

                Catch ex As Exception

                End Try
            Next
        Next
    End Sub
    Private Sub Guarda_Ctas()
        Dim sql As String = ""
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaPM.RowCount - 1
        Me.Cursor = Cursors.AppStarting
        For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
            If Trim(Me.TablaPM.Item(Suma.Index, i).Value) <> "" And Me.TablaPM.Item(Suma.Index, i).Value IsNot Nothing Then


                sql = "INSERT INTO dbo.ImpuestosPMPF 	
                            ( Descripcion, Cuenta,Naturaleza,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre,Anual,Anio,Id_Empresa,Suma,Operacion,Tipo,PM,Id_Estado)
                        	 VALUES 	( '" & Trim(Me.TablaPM.Item(Descrip.Index, i).Value) & "','" & Trim(Me.TablaPM.Item(Cta.Index, i).Value) & "', '" & Trim(Me.TablaPM.Item(Nat.Index, i).Value) & "',	" & IIf(IsDBNull(Me.TablaPM.Item(Enero.Index, i).Value) = True, 0, Me.TablaPM.Item(Enero.Index, i).Value) & ",	
                        " & IIf(IsDBNull(Me.TablaPM.Item(Febrero.Index, i).Value) = True, 0, Me.TablaPM.Item(Febrero.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Marzo.Index, i).Value) = True Or Me.TablaPM.Item(Marzo.Index, i).Value Is Nothing, 0, Me.TablaPM.Item(Marzo.Index, i).Value) & "," & IIf(IsDBNull(Me.TablaPM.Item(Abril.Index, i).Value) = True Or Me.TablaPM.Item(Abril.Index, i).Value Is Nothing, 0, Me.TablaPM.Item(Abril.Index, i).Value) & ",	
                        " & IIf(IsDBNull(Me.TablaPM.Item(Mayo.Index, i).Value) = True, 0, Me.TablaPM.Item(Mayo.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Junio.Index, i).Value) = True, 0, Me.TablaPM.Item(Junio.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Julio.Index, i).Value) = True, 0, Me.TablaPM.Item(Julio.Index, i).Value) & ",
                        " & IIf(IsDBNull(Me.TablaPM.Item(Agosto.Index, i).Value) = True, 0, Me.TablaPM.Item(Agosto.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Septiembre.Index, i).Value) = True, 0, Me.TablaPM.Item(Septiembre.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Octubre.Index, i).Value) = True, 0, Me.TablaPM.Item(Octubre.Index, i).Value) & ",
                        " & IIf(IsDBNull(Me.TablaPM.Item(Noviembre.Index, i).Value) = True, 0, Me.TablaPM.Item(Noviembre.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaPM.Item(Diciembre.Index, i).Value) = True, 0, Me.TablaPM.Item(Diciembre.Index, i).Value) & "," & IIf(IsDBNull(Me.TablaPM.Item(Anual.Index, i).Value) = True, 0, Me.TablaPM.Item(Anual.Index, i).Value) & ",	" & Trim(comboAño.Text) & ",	" & Me.lstCliente.SelectItem & "," & Trim(Me.TablaPM.Item(Suma.Index, i).Value) & "," & Trim(Me.TablaPM.Item(Operacion.Index, i).Value) & ", " & Trim(Me.TablaPM.Item(Tipo.Index, i).Value) & ",1," & Me.LstEstados.SelectItem & ")
"
                If Eventos.Comando_sql(sql) = 0 Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("No se guardo la informacion error en " & Trim(Me.TablaPM.Item(Descrip.Index, i).Value) & " verifique la Informacion ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    sql = "Delete From ImpuestosPMPF where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.comboAño.Text) & " and Pm = 1 and Id_Estado = " & Me.LstEstados.SelectItem & " "
                    If Eventos.Comando_sql(sql) = 0 Then
                        Exit Sub
                    End If
                End If
            End If
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        SP2.RunWorkerAsync(Me.TablaPM)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.TablaPM.Enabled = True
    End Sub
    Private Sub Recalcular(ByVal Titulo As Integer)
        Dim Mes() As String
        For i As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
            If Mes Is Nothing Then
                ReDim Preserve Mes(0)
                Mes(0) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            Else
                ReDim Preserve Mes(UBound(Mes) + 1)
                Mes(UBound(Mes)) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            End If

        Next

        Try
            Dim Col As Integer = Enero.Index + IIf(Convert.ToInt32(Me.ComboMes.Text) = 1, 0, Convert.ToInt32(Me.ComboMes.Text) - 1)
            Dim Valor As Decimal = 0
            'Dim Valor34 As Decimal = Calcula34()
            Dim Valor34 As Decimal = Calcula34()
            Dim Valores(78) As Decimal
            Dim Calculo64(6) As Decimal
            Dim a, b, c, d, g As Decimal
            For Each MS As String In Mes
                For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
                    Select Case Trim(Me.TablaPM.Item(Suma.Index, i).Value.ToString)
                        Case "3"
                            Valor = CalculaA(1, i, Col)
                            'For f As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "1.1" Then
                            '        Valor -= IIf(IsDBNull(Me.TablaPM.Item(Col, f).Value) = True, 0, Me.TablaPM.Item(Col, f).Value)
                            '        Exit For
                            '    End If
                            'Next
                            'For f As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "1.2" Then
                            '        Valor -= IIf(IsDBNull(Me.TablaPM.Item(Col, f).Value) = True, 0, Me.TablaPM.Item(Col, f).Value)
                            '        Exit For
                            '    End If
                            'Next
                            Me.TablaPM.Item(Col, i).Value = Valor
                        Case "2"
                            If Col > 3 Then


                                For f As Integer = 0 To Me.TablaPM.Rows.Count - 1
                                    If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "2" Then
                                        Valor = IIf(IsDBNull(Me.TablaPM.Item(Col - 1, f + 2).Value) = True, 0, Me.TablaPM.Item(Col - 1, f + 2).Value)
                                        Exit For
                                    End If
                                Next

                                Me.TablaPM.Item(Col, i).Value = Valor
                            End If
                        Case "4"
                            If Col > 3 Then
                                Me.TablaPM.Item(Col, i).Value = Me.TablaPM.Item(Col, i - 1).Value + Me.TablaPM.Item(Col, i - 2).Value
                            Else
                                For f As Integer = 10 To Me.TablaPM.Rows.Count - 1
                                    If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "3" Then
                                        Me.TablaPM.Item(Col, i).Value = IIf(IsDBNull(Me.TablaPM.Item(Col, f).Value), 0, Me.TablaPM.Item(Col, f).Value)
                                        Exit For
                                    End If
                                Next
                            End If

                        Case "5"
                            Me.TablaPM.Item(Col, i).Value = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "6"
                            For f As Integer = 10 To Me.TablaPM.Rows.Count - 1
                                If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "4" Then
                                    Me.TablaPM.Item(Col, i).Value = IIf(IsDBNull(Me.TablaPM.Item(Col, f).Value), 0, Me.TablaPM.Item(Col, f).Value)

                                End If
                                If Trim(Me.TablaPM.Item(Suma.Index, f).Value.ToString) = "5" Then
                                    Me.TablaPM.Item(Col, i).Value = Math.Round(Me.TablaPM.Item(Col, i).Value * Me.TablaPM.Item(Col, f).Value, 0)
                                End If
                            Next
                            Valores(0) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "7"
                            Valores(1) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "8"
                            If Col > 3 Then
                                Me.TablaPM.Item(Col, i).Value = IIf(IsDBNull(Me.TablaPM.Item(Col - 1, i).Value), 0, Me.TablaPM.Item(Col - 1, i).Value)
                                Valores(2) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                            Else
                                Valores(2) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                            End If
                        Case "9"
                            Me.TablaPM.Item(Col, i).Value = Col - 2
                            Valores(3) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "10"
                            Me.TablaPM.Item(Col, i).Value = Valores(2) * Valores(3)
                            Valores(4) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "11"
                            Me.TablaPM.Item(Col, i).Value = Math.Round(Valores(0) - Valores(1) + Valores(4), 0)
                            Valores(5) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "12"
                            Valores(6) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "13"
                            Valores(7) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "14"
                            Valores(8) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "15"
                            Valores(9) = Math.Round(IIf(Valores(5) - Valores(6) - Valores(7) - Valores(8) < 0, 0, Valores(5) - Valores(6) - Valores(7) - Valores(8)), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(9)
                        Case "16"
                            Valores(10) = 0.3
                            Me.TablaPM.Item(Col, i).Value = 0.3
                        Case "17"
                            Valores(11) = Math.Round(Valores(9) * Valores(10), 0)
                            Me.TablaPM.Item(Col, i).Value = Math.Round(Valores(11), 0)
                        Case "18"
                            Valores(12) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "19"

                            If Col > 3 Then
                                For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                                    If UCase(Me.TablaPM.Item(Nat.Index, F).Value.ToString().Trim()) = "SI" Then
                                        Valores(13) = Me.TablaPM.Item(Col - 1, i + 9).Value + Me.TablaPM.Item(Col - 1, i).Value
                                        Me.TablaPM.Item(Col, i).Value = Valores(13)
                                        Exit For
                                    ElseIf UCase(Me.TablaPM.Item(Nat.Index, F).Value.ToString().Trim()) = "NO" Then
                                        Valores(13) = Me.TablaPM.Item(Col - 1, i + 5).Value + Me.TablaPM.Item(Col - 1, i).Value
                                        Me.TablaPM.Item(Col, i).Value = Valores(13)
                                        Exit For
                                    End If
                                Next
                            Else
                                Valores(13) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                                Me.TablaPM.Item(Col, i).Value = Valores(13)
                            End If



                        Case "20"
                            Valores(14) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "21"
                            Valores(15) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "22"
                            Valores(16) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "23"
                            Valores(17) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "24"

                            Valores(18) = Math.Round(IIf(Valores(11) - Valores(12) - Valores(13) - Valores(14) - Valores(15) + Valores(16) - Valores(17) < 0, 0, Valores(11) - Valores(12) - Valores(13) - Valores(14) - Valores(15) + Valores(16) - Valores(17)), 0)
                            Me.TablaPM.Item(Col, i).Value = Math.Round(Valores(18), 0)
                        Case "25"
                            Valores(19) = Math.Round(IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value), 0)
                        Case "26"
                            Valores(20) = Math.Round(Valores(18) - Valores(19), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(20)
                        Case "27"
                            Valores(21) = Math.Round(IIf(Valores(20) > 0, Valores(20), 0), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(21)
                        Case "28"
                            Try
                                If UCase(Me.TablaPM.Item(Nat.Index, i).Value.ToString().Trim()) = "SI" Then
                                    Me.TablaPM.Item(Col, i).Value = Valores(19)
                                Else
                                    Me.TablaPM.Item(Col, i).Value = Valores(18)
                                End If
                            Catch ex As Exception
                                Me.TablaPM.Item(Col, i).Value = Valores(18)
                            End Try

                            Valores(22) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "29.1"
                            Valor = CalculaA(29, i, Col)
                            Valores(23) = Valor
                            Me.TablaPM.Item(Col, i).Value = Valor
                        Case "30.1"
                            Valor = Calcula(30, i, Col)
                            Valores(24) = Valor
                            Me.TablaPM.Item(Col, i).Value = Valor
                        Case "31"
                            Valores(25) = Valores(23) - Valores(24)
                            Me.TablaPM.Item(Col, i).Value = Valores(25)
                        Case "32.1"
                            Valor = CalculaT(32, i, Col)
                            Valores(26) = Valor
                            Me.TablaPM.Item(Col, i).Value = Valor
                        Case "33"
                            Valores(27) = Valores(25) - Valores(26)
                            Me.TablaPM.Item(Col, i).Value = Valores(27)
                        Case "34"
                            If Col = 3 Then
                                If Valor34 > Valores(27) And Valor34 > 0 Then
                                    Valores(28) = Valores(27)
                                ElseIf Valor34 > 0 And Valor34 < Valores(27) Then
                                    Valores(28) = Valor34
                                End If
                            Else
                                If Me.TablaPM.Item(Col - 1, i + 2).Value > Valores(27) Then 'And Me.TablaPM.Item(Col - 1, i + 2).Value > 0 
                                    Valores(28) = Valores(27)
                                ElseIf Me.TablaPM.Item(Col - 1, i + 2).Value < Valores(27) Then
                                    Valores(28) = Me.TablaPM.Item(Col - 1, i + 2).Value
                                End If
                            End If


                            Me.TablaPM.Item(Col, i).Value = Valores(28)
                        Case "35"
                            Valores(29) = Valores(27) - Valores(28)
                            Me.TablaPM.Item(Col, i).Value = Valores(29)
                        Case "36"
                            If Col = 3 Then
                                Valores(30) = Valor34 - Valores(28)
                            Else
                                Valores(30) = IIf(Me.TablaPM.Item(Col - 1, i + 2).Value < 0, Me.TablaPM.Item(Col - 1, i + 2).Value * -1, Me.TablaPM.Item(Col - 1, i + 2).Value) - Valores(28)
                            End If

                            Me.TablaPM.Item(2, i).Value = Valor34
                            Me.TablaPM.Item(Col, i).Value = Valores(30)
                        Case "37"
                            Try
                                Me.TablaPM.Item(Col, i).Value = IIf(Valores(27) = 0, "0", IIf(Valores(27) < 0, "A Favor", IIf(Valores(28) >= Valores(27), "Acreditamiento", IIf(Valores(29) < 0, "A Favor", "A Pagar"))))
                            Catch ex As Exception

                            End Try

                        Case "38"
                            Valores(31) = IIf(Valores(27) < 0, Valores(27), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(31)
                        Case "39"
                            Valores(32) = IIf(Valores(29) > 0, Valores(29), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(32)
                        Case "40"
                            Valores(33) = IIf(Valores(18) > 0, Valores(18), 0)
                            Me.TablaPM.Item(Col, i).Value = Valores(33)
                        Case "41"
                            Valores(34) = Valores(32)
                            Me.TablaPM.Item(Col, i).Value = Valores(34)
                        Case "42.1"
                            Valor = Calcula(42, i, Col) + Valores(33) + Valores(34)
                            Valores(35) = Valor
                            Me.TablaPM.Item(Col, i).Value = Valores(35)
                        Case "43"
                            Try
                                Valores(36) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                            Catch ex As Exception

                            End Try

                        Case "44"
                            Valores(37) = IIf(Valores(33) > Valores(36), Valores(36), Valores(33))
                            Me.TablaPM.Item(Col, i).Value = Valores(37)
                        Case "45"
                            Valores(38) = 0
                        Case "46"



                            Valores(39) = IIf(a > (Valores(36) - Valores(37) - Valores(38)), (Valores(36) - Valores(37) - Valores(38)), a)
                            Me.TablaPM.Item(Col, i).Value = Valores(39)
                            '        Exit For

                            'Next
                        Case "47"
                            'For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "42" Then
                            '        Select Case Trim(UCase(Me.TablaPM.Item(Cta.Index, F).Value.ToString()))
                            '            Case "2160-0002"
                            b = Me.TablaPM.Item(Col, i).Value
                            Valores(40) = IIf(b > (Valores(36) - Valores(37) - Valores(38) - Valores(39)), (Valores(36) - Valores(37) - Valores(38) - Valores(39)), b)
                            Me.TablaPM.Item(Col, i).Value = Valores(40)
                            '                Exit For
                            '        End Select
                            '    End If
                            'Next

                        Case "48"
                            'For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "42" Then
                            '        Select Case Trim(UCase(Me.TablaPM.Item(Cta.Index, F).Value.ToString()))

                            '            Case "2160-0003"
                            c = Me.TablaPM.Item(Col, i).Value
                            Valores(41) = IIf(c > (Valores(36) - Valores(37) - Valores(38) - Valores(39) - Valores(40)), (Valores(36) - Valores(37) - Valores(38) - Valores(39) - Valores(40)), c)
                            Me.TablaPM.Item(Col, i).Value = Valores(41)
                            '                Exit For
                            '        End Select
                            '    End If
                            'Next

                        Case "49"
                            'For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "42" Then
                            '        Select Case Trim(UCase(Me.TablaPM.Item(Cta.Index, F).Value.ToString()))
                            '            Case "2160-0004"
                            d = Me.TablaPM.Item(Col, i).Value
                            Valores(42) = IIf(d > (Valores(36) - Valores(37) - Valores(38) - Valores(39) - Valores(40) - Valores(41)), (Valores(36) - Valores(37) - Valores(38) - Valores(39) - Valores(40) - Valores(41)), d)
                            Me.TablaPM.Item(Col, i).Value = Valores(42)
                            '                Exit For
                            '        End Select
                            '    End If
                            'Next
                        Case "50"
                            'For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                            '    If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "42" Then
                            '        Select Case Trim(UCase(Me.TablaPM.Item(Cta.Index, F).Value.ToString()))
                            '            Case "2160-0010"
                            g = 0
                            Valores(43) = g
                            '                Exit For
                            '        End Select
                            '    End If
                            'Next

                        Case "51"
                            Try
                                Valores(44) = Valores(37) + Valores(38) + Valores(39) + Valores(40) + Valores(41) + Valores(42)
                                Me.TablaPM.Item(Col, i).Value = Valores(44)
                            Catch ex As Exception

                            End Try

                        Case "52"
                            Valores(45) = Valores(33) - Valores(37)
                            Me.TablaPM.Item(Col, i).Value = Valores(45)
                        Case "53"
                            Valores(46) = Valores(34) - Valores(38)
                            Me.TablaPM.Item(Col, i).Value = Valores(46)
                        Case "54"
                            For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                                If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "42" Then
                                    a = Me.TablaPM.Item(Col, F).Value
                                    b = Me.TablaPM.Item(Col, F + 1).Value
                                    c = Me.TablaPM.Item(Col, F + 2).Value
                                    d = Me.TablaPM.Item(Col, F + 3).Value
                                    g = Me.TablaPM.Item(Col, F + 4).Value
                                    Exit For
                                End If
                            Next

                            Valores(47) = a - Valores(39)
                            Me.TablaPM.Item(Col, i).Value = Valores(47)
                        Case "55"
                            Valores(48) = b - Valores(40)
                            Me.TablaPM.Item(Col, i).Value = Valores(48)
                        Case "56"
                            Valores(49) = c - Valores(41)
                            Me.TablaPM.Item(Col, i).Value = Valores(49)
                        Case "57"
                            Valores(50) = d - Valores(42)
                            Me.TablaPM.Item(Col, i).Value = Valores(50)
                        Case "58"
                            Valores(51) = g - Valores(43)
                            Me.TablaPM.Item(Col, i).Value = Valores(51)
                        Case "59"
                            Valores(52) = Valores(45) + Valores(46) + Valores(47) + Valores(48) + Valores(49) + Valores(50) + Valores(51)
                            Me.TablaPM.Item(Col, i).Value = Valores(52)

                        Case "60.2"
                            Valores(53) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)
                        Case "60.3"
                            Valores(54) = IIf(IsDBNull(Me.TablaPM.Item(Col, i).Value), 0, Me.TablaPM.Item(Col, i).Value)

                        Case "60.1"
                            Valor = Calcula(60, i, Col)
                            Valores(55) = Valor + Valores(53) + Valores(54)
                            Me.TablaPM.Item(Col, i).Value = Valores(55)
                        Case "61"
                            For F As Integer = 0 To Me.TablaPM.Rows.Count - 1
                                If Trim(Me.TablaPM.Item(Suma.Index, F).Value.ToString) = "60" Then

                                    Calculo64(0) = IIf(IsDBNull(Me.TablaPM.Item(Col, F).Value), 0, Me.TablaPM.Item(Col, F).Value)
                                    Calculo64(1) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 1).Value), 0, Me.TablaPM.Item(Col, F + 1).Value)
                                    Calculo64(2) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 2).Value), 0, Me.TablaPM.Item(Col, F + 2).Value)
                                    Calculo64(3) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 3).Value), 0, Me.TablaPM.Item(Col, F + 3).Value)
                                    Calculo64(4) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 4).Value), 0, Me.TablaPM.Item(Col, F + 4).Value)
                                    Calculo64(5) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 5).Value), 0, Me.TablaPM.Item(Col, F + 5).Value)
                                    Calculo64(6) = IIf(IsDBNull(Me.TablaPM.Item(Col, F + 6).Value), 0, Me.TablaPM.Item(Col, F + 6).Value)
                                    Exit For

                                End If
                            Next
                            Valores(56) = Valores(45) - Calculo64(0)
                            Me.TablaPM.Item(Col, i).Value = Valores(56)
                        Case "62"
                            Valores(57) = Valores(46) - Calculo64(1)
                            Me.TablaPM.Item(Col, i).Value = Valores(57)
                        Case "63"

                            Valores(59) = Valores(47) - Calculo64(2)
                            Me.TablaPM.Item(Col, i).Value = Valores(59)
                        Case "64"

                            Valores(61) = Valores(48) - Calculo64(3)
                            Me.TablaPM.Item(Col, i).Value = Valores(61)
                        Case "65"

                            Valores(63) = Valores(49) - Calculo64(4)
                            Me.TablaPM.Item(Col, i).Value = Valores(63)
                        Case "66"

                            Valores(65) = Valores(50) - Calculo64(5)
                            Me.TablaPM.Item(Col, i).Value = Valores(65)
                        Case "67"

                            Valores(67) = Valores(51) - Calculo64(6)
                            Me.TablaPM.Item(Col, i).Value = Valores(67)
                        Case "68"
                            Valores(68) = Valores(56) + Valores(57) + Valores(59) + Valores(61) + Valores(63) + Valores(65) + Valores(67)
                            Me.TablaPM.Item(Col, i).Value = Valores(68)
                        Case "69.1"
                            Valor = Calcula(69, i, Col)
                            Valores(69) = Valor
                            Me.TablaPM.Item(Col, i).Value = Valor
                        Case "70"
                            If Me.RadTasa.Checked = True Then
                                Valores(70) = 0
                                Me.TablaPM.Item(Col, i).Value = Valores(70)
                            Else
                                Valor = CalculaTarifa(Valores(69), i, Col)
                                Valores(70) = Valor
                                Me.TablaPM.Item(Col, i).Value = Valor
                            End If

                        Case "71"
                            'Valores(71) = Valores(69) - Valores(70)
                            'Me.TablaPM.Item(Col, i).Value = Valores(71)
                            If Me.RadTasa.Checked = True Then
                                Valores(71) = 0
                                Me.TablaPM.Item(Col, i).Value = Valores(71)
                            Else
                                Valores(71) = Me.TablaPM.Item(Col, i).Value
                            End If

                        Case "72"
                            '    Valor = CalculaTarifa(Valores(69), 5)
                            '    Valores(72) = Valor
                            '    Me.TablaPM.Item(Col, i).Value = Valor
                            If Me.RadTasa.Checked = True Then
                                Valores(72) = 0
                                Me.TablaPM.Item(Col, i).Value = Valores(72)
                            Else
                                Valores(72) = Me.TablaPM.Item(Col, i).Value
                            End If

                        Case = "73"
                            If Me.RadTasa.Checked = True Then
                                Valores(73) = 0
                                Me.TablaPM.Item(Col, i).Value = Valores(73)
                            Else
                                Valores(73) = Me.TablaPM.Item(Col, i).Value
                            End If
                        Case = "74"
                            '    Valor = CalculaTarifa(Valores(69), 3)
                            '    Valores(74) = Valor
                            '    Me.TablaPM.Item(Col, i).Value = Valor
                            If Me.RadTasa.Checked = True Then
                                Valores(74) = 0
                                Me.TablaPM.Item(Col, i).Value = Valores(74)
                            Else
                                Valores(74) = Me.TablaPM.Item(Col, i).Value
                            End If
                        Case = "75"
                            '    Valor = Math.Round(Valores(73) + Valores(74), 0)
                            '    Valores(75) = Valor
                            '    Me.TablaPM.Item(Col, i).Value = Valor
                            If Me.RadTasa.Checked = True Then
                                Valores(75) = 0
                                Me.TablaPM.Item(Col, i).Value = Math.Round(Valores(75), 0)
                            Else
                                Valores(75) = Me.TablaPM.Item(Col, i).Value
                            End If
                        Case = "76" ' Se debe asignar la tasa

                            'Valor = Math.Round((Valores(73) + Valores(74)) * 0.3, 0)
                            If Me.RadTasa.Checked = True Then
                                Valor = CalculaTasa()
                                Valores(76) = Valor
                                Me.TablaPM.Item(Col, i).Value = Valor
                            Else
                                Valor = 0
                                Valores(76) = Valor
                                Me.TablaPM.Item(Col, i).Value = Valor
                            End If

                        Case = "77"
                            If Me.RadTasa.Checked = True Then
                                Valor = Math.Round((Valores(69) * CalculaTasa()), 0)
                                Valores(77) = Valor
                                Me.TablaPM.Item(Col, i).Value = Valor
                            Else
                                Valor = Math.Round(Valores(75), 0)
                                Valores(77) = Valor
                                Me.TablaPM.Item(Col, i).Value = Valor
                            End If

                    End Select
                    'frm.Barra.Value = i
                Next
                Col += 1
            Next
        Catch ex As Exception

        End Try
        Try
            If Titulo = 1 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
            Me.Cursor = Cursors.Arrow
        Catch ex As Exception

        End Try
    End Sub
    Private Function Calcula34()
        Dim Importe As Decimal = 0
        Dim cuenta As String = ""
        For I As Integer = 60 To 80
            If UCase(Me.TablaPM.Item(Suma.Index, I).Value.ToString().Trim()) = "36" Then
                cuenta = UCase(Me.TablaPM.Item(Cta.Index, I).Value.ToString().Trim()).Replace("-", "")
            End If
        Next
        Importe = Eventos.Calcula_Saldos_Iniciales(cuenta.PadRight(16, "0"), Me.comboAño.Text.Trim() - 1, Me.lstCliente.SelectItem)
        Return Importe
    End Function
    Private Function CalculaTarifa(ByVal valor As Decimal, ByVal Fila As Integer, ByVal Columna As Integer)
        Dim Importe As Decimal = 0
        Dim Sql As String = "Select Limite_Inferior ,Limite_Superior  ,Cuota_Fija  	,PrcAplicacion from TablaImpuestosNominas where   Anio = " & Me.comboAño.Text.Trim() & "  AND id_estado = " & Me.LstEstados.SelectItem & "   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Try

                    If valor >= ds.Tables(0).Rows(i)(0) And (valor <= ds.Tables(0).Rows(i)(1) Or ds.Tables(0).Rows(i)(1) = 0) Then
                        Importe = ((valor - ds.Tables(0).Rows(i)(0)) * ds.Tables(0).Rows(i)(3)) + ds.Tables(0).Rows(i)(2)


                        Me.TablaPM.Item(Columna, Fila).Value = ds.Tables(0).Rows(i)(0)
                        Me.TablaPM.Item(Columna, Fila + 1).Value = valor - ds.Tables(0).Rows(i)(0)
                        Me.TablaPM.Item(Columna, Fila + 2).Value = IIf(ds.Tables(0).Rows(i)(3) > 1, ds.Tables(0).Rows(i)(3) / 100, ds.Tables(0).Rows(i)(3))
                        Me.TablaPM.Item(Columna, Fila + 3).Value = Me.TablaPM.Item(Columna, Fila + 1).Value * Me.TablaPM.Item(Columna, Fila + 2).Value
                        Me.TablaPM.Item(Columna, Fila + 4).Value = ds.Tables(0).Rows(i)(2)
                        Me.TablaPM.Item(Columna, Fila + 5).Value = Me.TablaPM.Item(Columna, Fila + 4).Value + Me.TablaPM.Item(Columna, Fila + 3).Value
                        Exit For
                    End If
                Catch ex As Exception
                    Importe = 0
                End Try
            Next
        End If

        Return Importe
    End Function
    Private Function CalculaTasa()
        My.Forms.Inicio.LblUsuario.Text = USR
        Dim Importe As Decimal = 0
        Dim Sql As String = "Select Importe  from TasasParaImpuestos where   Anio = " & Me.comboAño.Text.Trim() & "  AND Id_Estado = " & Me.LstEstados.SelectItem & "   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Try
                    Importe = IIf(IsDBNull(ds.Tables(0).Rows(i)(0)) = True, 0, ds.Tables(0).Rows(i)(0))
                Catch ex As Exception
                    Importe = 0
                End Try
            Next
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("No se ha dado de alta la Tasa del año " & Me.comboAño.Text.Trim() & " para el Estado de " & Me.LstEstados.SelectText & " deseas darla de alta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If Permiso(Me.Tag.ToString) Then
                    Importe = InputBox("Teclea el Importe de la Tasa:", 0)
                    If Importe > 0 Then
                        Sql = "INSERT INTO dbo.TasasParaImpuestos"
                        Sql &= "("
                        Sql &= "  Importe,Anio,"
                        Sql &= "  Id_Estado"
                        Sql &= "	)"
                        Sql &= "VALUES "
                        Sql &= "("
                        Sql &= "	" & Importe & ","
                        Sql &= "	" & Me.comboAño.Text.Trim() & ","
                        Sql &= " " & Me.LstEstados.SelectItem & ""
                        Sql &= "	)"
                        If Eventos.Comando_sql(Sql) > 0 Then
                            Eventos.Insertar_usuariol("IPM", Sql)
                        End If
                    Else Importe = 0

                    End If
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If

            Else
                Importe = 0
            End If
        End If
        Return Importe
    End Function
    Private Function Calcula(ByVal Tip As String, ByVal Fila As Integer, ByVal Columna As Integer)
        Dim Total As Decimal = 0

        Try
            For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
                If Me.TablaPM.Item(Suma.Index, i).Value = Tip Then
                    Total += IIf(IsDBNull(Me.TablaPM.Item(Columna, i).Value) = True, 0, Me.TablaPM.Item(Columna, i).Value)
                End If
            Next
        Catch ex As Exception

        End Try
        Return Total

    End Function
    Private Function CalculaT(ByVal Tip As String, ByVal Fila As Integer, ByVal Columna As Integer)
        Dim Total As Decimal = 0

        Try
            For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
                If Me.TablaPM.Item(Suma.Index, i).Value = Tip Then
                    If UCase(Me.TablaPM.Item(Nat.Index, i).Value.ToString().Trim()) = "C" Then
                        Total += IIf(IsDBNull(Me.TablaPM.Item(Columna, i).Value) = True, 0, Me.TablaPM.Item(Columna, i).Value)
                    Else
                        Total -= IIf(IsDBNull(Me.TablaPM.Item(Columna, i).Value) = True, 0, Me.TablaPM.Item(Columna, i).Value)
                    End If

                End If
            Next
        Catch ex As Exception

        End Try
        Return Total

    End Function
    Private Function CalculaA(ByVal Tip As String, ByVal Fila As Integer, ByVal Columna As Integer)
        Dim Total As Decimal = 0

        Try
            For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
                If Me.TablaPM.Item(Suma.Index, i).Value = Tip Then
                    If UCase(Me.TablaPM.Item(Nat.Index, i).Value.ToString().Trim()) = "A" Then
                        Total += IIf(IsDBNull(Me.TablaPM.Item(Columna, i).Value) = True, 0, Me.TablaPM.Item(Columna, i).Value)
                    Else
                        Total -= IIf(IsDBNull(Me.TablaPM.Item(Columna, i).Value) = True, 0, Me.TablaPM.Item(Columna, i).Value)
                    End If

                End If
            Next
        Catch ex As Exception

        End Try
        Return Total

    End Function

    Private Sub Calcular(ByVal Periodo As String, ByVal Id_Empresa As Integer, ByVal Cta As String, ByVal Fila As Integer, ByVal Col As Integer, ByVal Nat As String)
        Dim Saldo_Inicial As Decimal = 0
        Dim where As String = ""
        Cta = Trim(Cta.Replace("-", ""))
        Cta = Cta.TrimEnd("0")

        Select Case Len(Cta)
            Case 3, 2, 1
                Cta = Cta.PadRight(4, "0")
                where = "   Nivel1 = '" & Trim(Cta) & "' and Nivel2 >= '0000' "
            Case 4

                where = "   Nivel1 = '" & Trim(Cta) & "' and Nivel2 >= '0000' "
            Case 7, 6, 5
                Cta = Cta.PadRight(8, "0")
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3 >= '0000' "
            Case 8
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3 >= '0000' "
            Case 11, 10, 9
                Cta = Cta.PadRight(12, "0")
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 >= '0000' "
            Case 12
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 >= '0000' "
            Case 15, 14, 13
                Cta = Cta.PadRight(16, "0")
                where = "   Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 ='" & Cta.Substring(12, 4) & "'  "
            Case 16
                where = "   Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 ='" & Cta.Substring(12, 4) & "'  "
        End Select


        Nat = Trim(Nat)
        Dim Tipo As String = ""
        Dim Campo As String = ""
        If Nat.Trim() = "C" Then
            Tipo = "Detalle_Polizas.Cargo <> 0"
            Campo = "Detalle_Polizas.Cargo"
            Saldo_Inicial = 0
        ElseIf Nat.Trim() = "A" Then
            Tipo = "Detalle_Polizas.Abono <> 0"
            Campo = "Detalle_Polizas.Abono"
            Saldo_Inicial = 0
        ElseIf Nat.Trim() = "SF" Then
            Tipo = "Detalle_Polizas.Cargo <> 0 or Detalle_Polizas.Abono <> 0"
            Campo = "  Detalle_Polizas.Cargo - Detalle_Polizas.Abono   "
            Cta = Cta.PadRight(16, "0")
            Cta = Replace(Cta, "-", "")

            Saldo_Inicial = Eventos.Calcula_Saldos_Iniciales(Cta.Substring(0, 16), Me.comboAño.Text.Trim() - 1, Me.lstCliente.SelectItem)

        Else
            Exit Sub
        End If
        Dim Simbolo As String = ""
        Dim Mes() As String

        For i As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
            If Mes Is Nothing Then
                ReDim Preserve Mes(0)
                Mes(0) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            Else
                ReDim Preserve Mes(UBound(Mes) + 1)
                Mes(UBound(Mes)) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            End If

        Next

        Try
            For Each MS As String In Mes
                Dim Sql As String = "CalculoImpuestos '" & Campo & "'," & Id_Empresa & ",'" & Tipo & "','" & Periodo & "','" & MS & "','" & where.Replace("'", "") & "'"
                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Nat.Trim() = "SF" Then
                        Saldo_Inicial += IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
                        Me.TablaPM.Item(Col, Fila).Value = Saldo_Inicial
                    Else
                        Me.TablaPM.Item(Col, Fila).Value = Saldo_Inicial + IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
                    End If

                Else
                    Me.TablaPM.Item(Col, Fila).Value = Saldo_Inicial + 0
                End If
                Col += 1
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TablaPM_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaPM.CellDoubleClick
        Try

            If Me.TablaPM.CurrentCell.ColumnIndex = Nat.Index Then
                If Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value <> "Diferencia" Then
                    ' creamos el control comboBox
                    Dim Combo As New DataGridViewComboBoxCell

                    ' rellenamos los items del combobox
                    Combo.Items.Add("C")
                    Combo.Items.Add("A")
                    Combo.Items.Add("SF")
                    Combo.FlatStyle = FlatStyle.Flat
                    ' le asignamos el control combobox a la celda
                    Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index) = Combo.Clone

                End If
            End If
            If Me.TablaPM.Item(Tipo.Index, Me.TablaPM.CurrentRow.Index).Value = 1 Then
                If Me.TablaPM.CurrentCell.ColumnIndex = Cta.Index Then
                    Dim Combo As New DataGridViewComboBoxCell
                    Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " order by Alias ")
                    If Cuentas.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                            Combo.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                        Next
                        Me.TablaPM.Item(Cta.Index, Me.TablaPM.CurrentRow.Index) = Combo.Clone
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub TablaPM_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaPM.KeyDown
        If e.KeyCode = Keys.Enter Then

            Try
                If Me.TablaPM.CurrentCell.ColumnIndex = Nat.Index Then
                    If Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value.ToString() <> "Diferencia" Then
                        Dim Combo As New DataGridViewTextBoxCell
                        Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index) = Combo.Clone
                        Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value = ""
                    End If
                End If
            Catch ex As Exception

            End Try
            Try
                If Me.TablaPM.CurrentCell.ColumnIndex = Cta.Index Then
                    Dim Combo As New DataGridViewTextBoxCell
                    Me.TablaPM.Item(Cta.Index, Me.TablaPM.CurrentRow.Index) = Combo.Clone
                    Me.TablaPM.Item(Cta.Index, Me.TablaPM.CurrentRow.Index).Value = ""

                End If
            Catch ex As Exception

            End Try
            Try

                For C As Integer = 3 To 15
                    Dim F As DataGridViewCell
                    F = TablaPM.Rows(Me.TablaPM.CurrentRow.Index).Cells(C)
                    F.Style.BackColor = Color.Yellow
                    F.Style.Format = "N2"
                    F.ReadOnly = False
                Next

            Catch ex As Exception

            End Try
        ElseIf e.KeyCode = Keys.W AndAlso e.Modifiers = Keys.Control Then
            Try
                If Me.TablaPM.CurrentCell.ColumnIndex = Nat.Index Then
                    For C As Integer = 3 To 15
                        Dim F As DataGridViewCell
                        F = TablaPM.Rows(Me.TablaPM.CurrentRow.Index).Cells(C)
                        F.Style.BackColor = Color.White
                        F.Style.Format = "N2"
                        F.ReadOnly = True
                    Next
                End If
            Catch ex As Exception

            End Try


        ElseIf e.KeyCode = Keys.R AndAlso e.Modifiers = Keys.Shift Then
            Dim Tipo As String = IIf(IsNothing(Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value.ToString) = True, "", Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value.ToString)
            Calcular("Polizas.Id_Anio  = " & Me.comboAño.Text() & "", Me.lstCliente.SelectItem, Me.TablaPM.Item(Cta.Index, Me.TablaPM.CurrentRow.Index).Value.ToString, Me.TablaPM.CurrentRow.Index, Enero.Index, Trim(Tipo))
            Recalcular(0)
        End If

    End Sub

    Private Sub TablaPM_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaPM.CellEndEdit
        Recalcular(0)
        Try
            Me.TablaPM.Item(Me.TablaPM.CurrentCell.ColumnIndex, Me.TablaPM.CurrentRow.Index).Value = Me.TablaPM.Item(Me.TablaPM.CurrentCell.ColumnIndex, Me.TablaPM.CurrentRow.Index).Value * 1
        Catch ex As Exception

        End Try
        Try
            If Me.TablaPM.Item(Suma.Index, Me.TablaPM.CurrentRow.Index).Value.ToString = "76" Then
                CambioTarifa()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CambioTarifa()


        Dim Sql As String = "Select Id_TasaImpuetsos,Importe  from TasasParaImpuestos where   Anio = " & Me.comboAño.Text.Trim() & "  AND id_estado = " & Me.LstEstados.SelectItem & "   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then

            Try
                If Permiso(Me.Tag.ToString) Then
                    If IsNumeric(Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value) Then
                        If Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value <> ds.Tables(0).Rows(0)(1) Then
                            Sql = "UPDATE dbo.TasasParaImpuestos SET  Importe = " & Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value & " WHERE Id_TasaImpuetsos =" & ds.Tables(0).Rows(0)(0) & " "
                            If Eventos.Comando_sql(Sql) > 0 Then
                                Eventos.Insertar_usuariol("TasasImp", Sql)
                            End If
                            Recalcular(0)
                        End If
                    End If
                Else
                    Me.TablaPM.Item(Nat.Index, Me.TablaPM.CurrentRow.Index).Value = ds.Tables(0).Rows(0)(1)
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub Cargar(ByVal Ds As DataSet)
        Try
            If Ds.Tables(0).Rows.Count > 0 Then

            End If
        Catch ex As Exception
            Exit Sub
        End Try
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Text = "Cargando Informacion por favor espere..."
        frm.Barra.Maximum = Me.TablaPM.Rows.Count

        For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.TablaPM.Rows(i)
            Try
                Me.TablaPM.Item(Descrip.Index, i).Value = Ds.Tables(0).Rows(i)("Descripcion")
            Catch ex As Exception

            End Try

            Try
                If Ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                    Me.TablaPM.Item(Cta.Index, i).Value = Obtener_Index(Trim(Ds.Tables(0).Rows(i)("Cuenta")), Me.Cta)
                    'Fila.Cells(Cta.Index).Value = Me.Cta.Items(Obtener_Index(Trim(Ds.Tables(0).Rows(i)("Cuenta")), Me.Cta))
                End If
            Catch ex As Exception

            End Try

            Try
                Me.TablaPM.Item(Nat.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Naturaleza")), "", Ds.Tables(0).Rows(i)("Naturaleza"))
                Me.TablaPM.Item(Enero.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Enero")), 0, Ds.Tables(0).Rows(i)("Enero"))
                Me.TablaPM.Item(Febrero.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Febrero")), 0, Ds.Tables(0).Rows(i)("Febrero"))
                Me.TablaPM.Item(Marzo.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Marzo")), 0, Ds.Tables(0).Rows(i)("Marzo"))
                Me.TablaPM.Item(Abril.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Abril")), 0, Ds.Tables(0).Rows(i)("Abril"))
                Me.TablaPM.Item(Mayo.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Mayo")), 0, Ds.Tables(0).Rows(i)("Mayo"))
                Me.TablaPM.Item(Junio.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Junio")), 0, Ds.Tables(0).Rows(i)("Junio"))
                Me.TablaPM.Item(Julio.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Julio")), 0, Ds.Tables(0).Rows(i)("Julio"))
                Me.TablaPM.Item(Agosto.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Agosto")), 0, Ds.Tables(0).Rows(i)("Agosto"))
                Me.TablaPM.Item(Septiembre.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Septiembre")), 0, Ds.Tables(0).Rows(i)("Septiembre"))
                Me.TablaPM.Item(Octubre.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Octubre")), 0, Ds.Tables(0).Rows(i)("Octubre"))
                Me.TablaPM.Item(Noviembre.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Noviembre")), 0, Ds.Tables(0).Rows(i)("Noviembre"))
                Me.TablaPM.Item(Diciembre.Index, i).Value = IIf(IsDBNull(Ds.Tables(0).Rows(i)("Diciembre")), 0, Ds.Tables(0).Rows(i)("Diciembre"))
                Me.TablaPM.Item(Anual.Index, i).Value = Ds.Tables(0).Rows(i)("Anual")
                Me.TablaPM.Item(Suma.Index, i).Value = Ds.Tables(0).Rows(i)("Suma")
                Me.TablaPM.Item(Operacion.Index, i).Value = Ds.Tables(0).Rows(i)("Operacion")
                Me.TablaPM.Item(Tipo.Index, i).Value = Ds.Tables(0).Rows(i)("Tipo")

                Try
                    Select Case Me.TablaPM.Item(Tipo.Index, i).Value.ToString()

                        Case "0"
                            PintaTabla(i, Me.TablaPM, 1)
                        Case "1.1"
                            PintaTabla(i, Me.TablaPM, 1)
                    End Select
                    Select Case Me.TablaPM.Item(Suma.Index, i).Value.ToString()
                        Case "2", "3", "4", "29.1", "30.1", "32.1", "42.1", "50", "59", "60.1", "68", "69.1", "75", "77"
                            PintaTabla(i, Me.TablaPM, 2)
                        Case "5"
                            PintaTabla(i, Me.TablaPM, 3)
                        Case "6"
                            PintaTabla(i, Me.TablaPM, 4)
                        Case "11", "17", "24", "26"
                            PintaTabla(i, Me.TablaPM, 4)
                        Case "7", "25", "12", "13", "14", "18", "20", "21", "22", "23", "60"
                            PintaTabla(i, Me.TablaPM, 5)
                        Case "37"
                            PintaTabla(i, Me.TablaPM, 6)
                    End Select

                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
            frm.Barra.Value = i
        Next
        frm.Close()


    End Sub
    Private Sub CagarPlantilla(ByVal Ds As DataSet)
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Text = "Calculando plantilla por favor espere..."
        frm.Barra.Maximum = Ds.Tables(0).Rows.Count
        Me.Cursor = Cursors.AppStarting

        For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.TablaPM.Rows(i)
            Me.TablaPM.Item(Descrip.Index, i).Value = Ds.Tables(0).Rows(i)("Descripcion")
            Try
                If Ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                    Fila.Cells(Cta.Index).Value = Obtener_Index(Trim(Ds.Tables(0).Rows(i)("Cuenta")), Me.Cta)
                End If
            Catch ex As Exception

            End Try


            Me.TablaPM.Item(Nat.Index, i).Value = Ds.Tables(0).Rows(i)("Naturaleza")
            Me.TablaPM.Item(Enero.Index, i).Value = Ds.Tables(0).Rows(i)("Enero")
            Me.TablaPM.Item(Febrero.Index, i).Value = Ds.Tables(0).Rows(i)("Febrero")
            Me.TablaPM.Item(Marzo.Index, i).Value = Ds.Tables(0).Rows(i)("Marzo")
            Me.TablaPM.Item(Abril.Index, i).Value = Ds.Tables(0).Rows(i)("Abril")
            Me.TablaPM.Item(Mayo.Index, i).Value = Ds.Tables(0).Rows(i)("Mayo")
            Me.TablaPM.Item(Junio.Index, i).Value = Ds.Tables(0).Rows(i)("Junio")
            Me.TablaPM.Item(Julio.Index, i).Value = Ds.Tables(0).Rows(i)("Julio")
            Me.TablaPM.Item(Agosto.Index, i).Value = Ds.Tables(0).Rows(i)("Agosto")
            Me.TablaPM.Item(Septiembre.Index, i).Value = Ds.Tables(0).Rows(i)("Septiembre")
            Me.TablaPM.Item(Octubre.Index, i).Value = Ds.Tables(0).Rows(i)("Octubre")
            Me.TablaPM.Item(Noviembre.Index, i).Value = Ds.Tables(0).Rows(i)("Noviembre")
            Me.TablaPM.Item(Diciembre.Index, i).Value = Ds.Tables(0).Rows(i)("Diciembre")
            Me.TablaPM.Item(Anual.Index, i).Value = Ds.Tables(0).Rows(i)("Anual")
            Me.TablaPM.Item(Suma.Index, i).Value = Ds.Tables(0).Rows(i)("Suma")
            Me.TablaPM.Item(Operacion.Index, i).Value = Ds.Tables(0).Rows(i)("Operacion")
            Me.TablaPM.Item(Tipo.Index, i).Value = Ds.Tables(0).Rows(i)("Tipo")
            frm.Barra.Value = i
        Next
        frm.Close()
        Guarda_Ctas()


        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub SP2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP2.DoWork
        If Me.LstEstados.SelectText <> "" Then
            My.Forms.Inicio.txtServerDB.Text = serV

#Region "Calculo de importes"
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Text = "Calculando Importes por favor espere..."
            frm.Barra.Maximum = Me.TablaPM.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To Me.TablaPM.Rows.Count - 1
                If Me.TablaPM.Item(Cta.Index, i).Value <> Nothing Then
                    If Me.TablaPM.Item(Nat.Index, i).Value <> Nothing And Me.TablaPM.Item(Suma.Index, i).Value <> "60" And Me.TablaPM.Item(Suma.Index, i).Value <> "61" And Me.TablaPM.Item(Suma.Index, i).Value <> "62" And Me.TablaPM.Item(Suma.Index, i).Value <> "63" And Me.TablaPM.Item(Suma.Index, i).Value <> "64" And Me.TablaPM.Item(Suma.Index, i).Value <> "65" And Me.TablaPM.Item(Suma.Index, i).Value <> "66" And Me.TablaPM.Item(Suma.Index, i).Value <> "67" Then
                        Dim Tipo As String = IIf(IsNothing(Me.TablaPM.Item(Nat.Index, i).Value.ToString) = True, "", Me.TablaPM.Item(Nat.Index, i).Value.ToString)
                        Calcular("Polizas.Id_Anio  = " & Me.comboAño.Text() & "", Me.lstCliente.SelectItem, Me.TablaPM.Item(Cta.Index, i).Value.ToString, i, Enero.Index, Trim(Tipo))
                    End If
                End If
                frm.Barra.Value = i
            Next
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Se han calculado los saldos Ahora se realizaran las Operaciones Correspondientes por favor espere...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            frm.Close()
#End Region
            Recalcular(1)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Se debe seleccionar el Estado para calcular las Tarifas Gracias...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub TablaPM_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaPM.DataError
        '  MessageBox.Show(e.Context.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Class
