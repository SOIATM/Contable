Imports Telerik.WinControls
Public Class Control_Masivo_Personal

    Public Event Registro(ByVal clave As String)
    Dim tiene As Boolean = False
    Dim clave As String = ""
    Dim activo As Boolean
    Private Sub Control_Masivo_Personal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_clientes()
        Eventos.DiseñoTabla(Me.Tabla)
        Eventos.DiseñoTabla(Me.TablaImportar)
        activo = False
    End Sub
    Private Sub Cargar_clientes()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1
        Me.LstDelegacion.Cargar("SELECT Delegaciones_IMSS.Id_Delegacion_IMSS, Delegaciones_IMSS.Delegacion FROM     Delegaciones_IMSS")
        Dim sql As String = "SELECT Delegaciones_IMSS.Id_Delegacion_IMSS  , Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS,Sub_Delegaciones_IMSS.Clave  FROM Delegaciones_IMSS 
                                    INNER JOIN Sub_Delegaciones_IMSS ON Sub_Delegaciones_IMSS.Id_Delegacion_IMSS =Delegaciones_IMSS.Id_Delegacion_IMSS 
                                    INNER JOIN Clientes_Delegaciones ON Clientes_Delegaciones.Id_Sub_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS 
                                    INNER JOIN Empresa ON Empresa.Id_Empresa = Clientes_Delegaciones.Id_Empresa 
                                    WHERE  Clientes_Delegaciones.Id_Empresa = " & My.Forms.Inicio.Clt & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.LstDelegacion.SelectItem = ds.Tables(0).Rows(0)(0)
            Me.LstSubDelegacion.Cargar("SELECT Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS, Sub_Delegaciones_IMSS.Sub_Delegacion   
                                    FROM Sub_Delegaciones_IMSS 
                                    INNER JOIN Delegaciones_IMSS ON Delegaciones_IMSS.Id_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Delegacion_IMSS 
                                    WHERE Delegaciones_IMSS.Id_Delegacion_IMSS =" & ds.Tables(0).Rows(0)(0) & "")
            Me.LstSubDelegacion.SelectItem = ds.Tables(0).Rows(0)(1)
            clave = ds.Tables(0).Rows(0)(2)
            tiene = True
        Else
            tiene = False
            Me.LstDelegacion.SelectText = ""
            RadMessageBox.Show("Selecciona parametros de Delegacion y Sub-Delegacion a la Empresa " & Me.lstCliente.SelectText & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If
        If Me.lstCliente.SelectText <> "" Then
            Dim consulta As String = "SELECT Personal_Clientes.Id_Persona_Cliente, Personal_Clientes.ID_matricula, Personal_Clientes.Nombres, Personal_Clientes.Ap_paterno, Personal_Clientes.Ap_materno, Personal_Clientes.Registro_Patronal, "
            consulta &= " Personal_Clientes.Dig_Verificador_RP, Personal_Clientes.No_IMSS, Personal_Clientes.Dig_Verif_IMSS, Personal_Clientes.Salario_Base, Personal_Clientes.Tipo_Trabajador,"
            consulta &= " Personal_Clientes.Tipo_Salario, Personal_Clientes.Sem_Jornada_Reducida, Personal_Clientes.Unidad_Medicina_Familiar, Personal_Clientes.Guia, "
            consulta &= " Personal_Clientes.CURP,   convert(datetime,Personal_Clientes.Fecha_alta ,103) AS Fecha_alta, convert(datetime,Personal_Clientes.Fecha_Baja ,103) AS Fecha_Baja  "
            consulta &= " FROM     Personal_Clientes INNER JOIN Empresa ON Personal_Clientes.Id_Empresa = Empresa.Id_Empresa where   Personal_Clientes.Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then

                Dim sql As String = "   SELECT DISTINCT  Registro_Patronal FROM Personal_Clientes WHERE Id_Empresa =  " & Me.lstCliente.SelectItem & "  "
                Dim RP As DataSet = Eventos.Obtener_DS(sql)
                If RP.Tables(0).Rows.Count > 0 Then

                    If Me.RegistroPatronal.Items.Count = 0 Then
                        For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                            Me.RegistroPatronal.Items.Add(RP.Tables(0).Rows(i)("Registro_Patronal"))
                        Next
                    Else

                    End If

                End If
                Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)

                    Me.TablaImportar.Item(Id.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Persona_Cliente")) = True, "", ds.Tables(0).Rows(j)("Id_Persona_Cliente"))
                    Me.TablaImportar.Item(Mat.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ID_matricula")) = True, "", ds.Tables(0).Rows(j)("ID_matricula"))
                    Me.TablaImportar.Item(Nombres.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Nombres")) = True, "", ds.Tables(0).Rows(j)("Nombres"))
                    Me.TablaImportar.Item(Appaterno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_paterno")) = True, "", ds.Tables(0).Rows(j)("Ap_paterno"))
                    Me.TablaImportar.Item(Ap_materno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_materno")) = True, "", ds.Tables(0).Rows(j)("Ap_materno"))
                    Try
                        If Trim(ds.Tables(0).Rows(j)("Registro_Patronal")) <> "" Then
                            Fila.Cells(RegistroPatronal.Index).Value = Me.RegistroPatronal.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Registro_Patronal")), Me.RegistroPatronal))
                        End If
                    Catch ex As Exception

                    End Try
                    Me.TablaImportar.Item(DigVerificadorRP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verificador_RP")) = True, "", ds.Tables(0).Rows(j)("Dig_Verificador_RP"))
                    Me.TablaImportar.Item(NoIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("No_IMSS")) = True, "", ds.Tables(0).Rows(j)("No_IMSS"))
                    Me.TablaImportar.Item(DigVerifIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verif_IMSS")) = True, "", ds.Tables(0).Rows(j)("Dig_Verif_IMSS"))
                    Me.TablaImportar.Item(SalarioBase.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Salario_Base")) = True, "", ds.Tables(0).Rows(j)("Salario_Base"))
                    Try
                        If Trim(ds.Tables(0).Rows(j)("Tipo_Trabajador")) <> "" Then
                            Fila.Cells(TipoTrabajador.Index).Value = Me.TipoTrabajador.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo_Trabajador")), Me.TipoTrabajador))
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(j)("Tipo_Salario")) <> "" Then
                            Fila.Cells(TipoSalario.Index).Value = Me.TipoSalario.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo_Salario")), Me.TipoSalario))
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")) <> "" Then
                            Fila.Cells(SemJornadaReducida.Index).Value = Me.SemJornadaReducida.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")), Me.SemJornadaReducida))
                        End If
                    Catch ex As Exception

                    End Try

                    Me.TablaImportar.Item(UnidadMedicinaFamiliar.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar")) = True, "", ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar"))
                    Me.TablaImportar.Item(Guia.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Guia")) = True, "", ds.Tables(0).Rows(j)("Guia"))
                    Me.TablaImportar.Item(CURP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CURP")) = True, "", ds.Tables(0).Rows(j)("CURP"))
                    Me.TablaImportar.Item(Fechaalta.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_alta")) = True, "", ds.Tables(0).Rows(j)("Fecha_alta"))
                    Me.TablaImportar.Item(Fecha_Baja.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(j)("Fecha_Baja"))
                    frm.Barra.Value = j
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            Else
                RadMessageBox.Show("No hay personal para " & Me.lstCliente.SelectText & " ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
        Else
            RadMessageBox.Show("Selecciona una Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub CmdMasivo_Click(sender As Object, e As EventArgs) Handles CmdMasivo.Click
        Eventos.Abrir_form(Carga_Masiva_Personal)
    End Sub
    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")

    End Sub
    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.lstCliente.SelectItem <> "" Then
            If Me.LblFiltro.Text <> "" Then
                Me.TablaImportar.Rows.Clear()

                Dim posicion As Integer = 0
                Dim consulta As String = "SELECT Personal_Clientes.Id_Persona_Cliente, Personal_Clientes.ID_matricula, Personal_Clientes.Nombres, Personal_Clientes.Ap_paterno, Personal_Clientes.Ap_materno, Personal_Clientes.Registro_Patronal, "
                consulta &= " Personal_Clientes.Dig_Verificador_RP, Personal_Clientes.No_IMSS, Personal_Clientes.Dig_Verif_IMSS, Personal_Clientes.Salario_Base, Personal_Clientes.Tipo_Trabajador,"
                consulta &= " Personal_Clientes.Tipo_Salario, Personal_Clientes.Sem_Jornada_Reducida, Personal_Clientes.Unidad_Medicina_Familiar, Personal_Clientes.Guia, "
                consulta &= " Personal_Clientes.CURP,   convert(datetime,Personal_Clientes.Fecha_alta ,103) AS Fecha_alta, convert(datetime,Personal_Clientes.Fecha_Baja ,103) AS Fecha_Baja  "
                consulta &= " FROM     Personal_Clientes INNER JOIN Empresa ON Personal_Clientes.Id_Empresa = Empresa.Id_Empresa where   Personal_Clientes.Id_Empresa = " & Me.lstCliente.SelectItem & " and  " & Me.LblFiltro.Text & " like '%" & Me.TxtFiltro.Text & "%' "
                Dim ds As DataSet = Eventos.Obtener_DS(consulta)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                    Dim sql As String = "   SELECT DISTINCT  Registro_Patronal FROM Personal_Clientes WHERE Id_Empresa =  " & Me.lstCliente.SelectItem & "  "
                    Dim RP As DataSet = Eventos.Obtener_DS(sql)
                    If RP.Tables(0).Rows.Count > 0 Then

                        If Me.RegistroPatronal.Items.Count = 0 Then
                            For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                                Me.RegistroPatronal.Items.Add(RP.Tables(0).Rows(i)("Registro_Patronal"))
                            Next
                        Else

                        End If

                    End If
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)

                        Me.TablaImportar.Item(Id.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Persona_Cliente")) = True, "", ds.Tables(0).Rows(j)("Id_Persona_Cliente"))
                        Me.TablaImportar.Item(Mat.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ID_matricula")) = True, "", ds.Tables(0).Rows(j)("ID_matricula"))
                        Me.TablaImportar.Item(Nombres.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Nombres")) = True, "", ds.Tables(0).Rows(j)("Nombres"))
                        Me.TablaImportar.Item(Appaterno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_paterno")) = True, "", ds.Tables(0).Rows(j)("Ap_paterno"))
                        Me.TablaImportar.Item(Ap_materno.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ap_materno")) = True, "", ds.Tables(0).Rows(j)("Ap_materno"))
                        Try
                            If Trim(ds.Tables(0).Rows(j)("Registro_Patronal")) <> "" Then
                                Fila.Cells(RegistroPatronal.Index).Value = Me.RegistroPatronal.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Registro_Patronal")), Me.RegistroPatronal))
                            End If
                        Catch ex As Exception

                        End Try
                        ' Me.TablaImportar.Item(RegistroPatronal.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Registro_Patronal")) = True, "", ds.Tables(0).Rows(j)("Registro_Patronal"))
                        Me.TablaImportar.Item(DigVerificadorRP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verificador_RP")) = True, "", ds.Tables(0).Rows(j)("Dig_Verificador_RP"))
                        Me.TablaImportar.Item(NoIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("No_IMSS")) = True, "", ds.Tables(0).Rows(j)("No_IMSS"))
                        Me.TablaImportar.Item(DigVerifIMSS.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dig_Verif_IMSS")) = True, "", ds.Tables(0).Rows(j)("Dig_Verif_IMSS"))
                        Me.TablaImportar.Item(SalarioBase.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Salario_Base")) = True, "", ds.Tables(0).Rows(j)("Salario_Base"))

                        Try
                            If Trim(ds.Tables(0).Rows(j)("Tipo_Trabajador")) <> "" Then
                                Fila.Cells(TipoTrabajador.Index).Value = Me.TipoTrabajador.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo_Trabajador")), Me.TipoTrabajador))
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            If Trim(ds.Tables(0).Rows(j)("Tipo_Salario")) <> "" Then
                                Fila.Cells(TipoSalario.Index).Value = Me.TipoSalario.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo_Salario")), Me.TipoSalario))
                            End If
                        Catch ex As Exception

                        End Try
                        Try
                            If Trim(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")) <> "" Then
                                Fila.Cells(SemJornadaReducida.Index).Value = Me.SemJornadaReducida.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Sem_Jornada_Reducida")), Me.SemJornadaReducida))
                            End If
                        Catch ex As Exception

                        End Try

                        Me.TablaImportar.Item(UnidadMedicinaFamiliar.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar")) = True, "", ds.Tables(0).Rows(j)("Unidad_Medicina_Familiar"))
                        Me.TablaImportar.Item(Guia.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Guia")) = True, "", ds.Tables(0).Rows(j)("Guia"))
                        Me.TablaImportar.Item(CURP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CURP")) = True, "", ds.Tables(0).Rows(j)("CURP"))
                        Me.TablaImportar.Item(Fechaalta.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_alta")) = True, "", ds.Tables(0).Rows(j)("Fecha_alta"))
                        Me.TablaImportar.Item(Fecha_Baja.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Fecha_Baja")) = True, "", ds.Tables(0).Rows(j)("Fecha_Baja"))

                    Next
                End If

            End If
        End If
    End Sub
    Private Sub TablaImportar_Click(sender As Object, e As EventArgs) Handles TablaImportar.Click
        If TablaImportar.RowCount > 0 Then
            RaiseEvent Registro(TablaImportar.Item(0, TablaImportar.CurrentCell.RowIndex).Value.ToString)
            Me.LblFiltro.Text = Me.TablaImportar.Columns(Me.TablaImportar.CurrentCell.ColumnIndex).HeaderText
        End If
    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Mat.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados de la Empresa : " & Me.lstCliente.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                            If Me.TablaImportar.Item(Id.Index, i).Value <> Nothing Then
                                If Eventos.Comando_sql("Delete from dbo.Personal_Clientes where Id_Persona_Cliente=" & Me.TablaImportar.Item(Id.Index, i).Value) > 0 Then
                                    RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                    Eventos.Insertar_usuariol("Id_Persona_Cliente", "Delete from dbo.Personal_Clientes where Id_Persona_Cliente=" & Me.TablaImportar.Item(Id.Index, i).Value & "")
                                Else
                                    RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                End If
                            End If
                        Next
                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If

    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(Id.Index, i).Value <> Nothing Then

                Edita_Registro(Me.TablaImportar.Item(Id.Index, i).Value, IIf(Me.TablaImportar.Item(Mat.Index, i).Value = "", 0, Me.TablaImportar.Item(Mat.Index, i).Value), Me.TablaImportar.Item(Appaterno.Index, i).Value,
                                Me.TablaImportar.Item(Ap_materno.Index, i).Value, Me.TablaImportar.Item(Nombres.Index, i).Value, Me.TablaImportar.Item(RegistroPatronal.Index, i).Value,
                                    Me.TablaImportar.Item(DigVerificadorRP.Index, i).Value, Me.TablaImportar.Item(NoIMSS.Index, i).Value, Me.TablaImportar.Item(DigVerifIMSS.Index, i).Value,
                                    Me.TablaImportar.Item(SalarioBase.Index, i).Value, Me.TablaImportar.Item(TipoTrabajador.Index, i).Value, Me.TablaImportar.Item(TipoSalario.Index, i).Value,
                                    Me.TablaImportar.Item(SemJornadaReducida.Index, i).Value, Me.TablaImportar.Item(UnidadMedicinaFamiliar.Index, i).Value, Me.TablaImportar.Item(Guia.Index, i).Value,
                                    Me.TablaImportar.Item(CURP.Index, i).Value, Me.TablaImportar.Item(Fechaalta.Index, i).Value, Me.TablaImportar.Item(Fecha_Baja.Index, i).Value)
            Else
                Inserta_Registro(0, Me.TablaImportar.Item(Mat.Index, i).Value, Me.TablaImportar.Item(Appaterno.Index, i).Value,
                                Me.TablaImportar.Item(Ap_materno.Index, i).Value, Me.TablaImportar.Item(Nombres.Index, i).Value, Me.TablaImportar.Item(RegistroPatronal.Index, i).Value,
                                    Me.TablaImportar.Item(DigVerificadorRP.Index, i).Value, Me.TablaImportar.Item(NoIMSS.Index, i).Value, Me.TablaImportar.Item(DigVerifIMSS.Index, i).Value,
                                    Me.TablaImportar.Item(SalarioBase.Index, i).Value, Me.TablaImportar.Item(TipoTrabajador.Index, i).Value, Me.TablaImportar.Item(TipoSalario.Index, i).Value,
                                    Me.TablaImportar.Item(SemJornadaReducida.Index, i).Value, Me.TablaImportar.Item(UnidadMedicinaFamiliar.Index, i).Value, Me.TablaImportar.Item(Guia.Index, i).Value,
                                    Me.TablaImportar.Item(CURP.Index, i).Value, Me.TablaImportar.Item(Fechaalta.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_Persona_Cliente As Integer, ByVal ID_matricula As Integer, ByVal Ap_paterno As String,
                               ByVal Ap_materno As String, ByVal Nombres As String, ByVal Registro_Patronal As String,
                               ByVal Dig_Verificador_RP As String, ByVal No_IMSS As String, ByVal Dig_Verif_IMSS As String,
                               ByVal Salario_Base As Decimal, ByVal Tipo_Trabajador As Integer, ByVal Tipo_Salario As Integer,
                                ByVal Sem_Jornada_Reducida As Integer, ByVal Unidad_Medicina_Familiar As String, ByVal Guia As String,
                                 ByVal CURP As String, ByVal Fecha_alta As String, ByVal Fecha_Baja As String)
        Dim sql As String = "UPDATE dbo.Personal_Clientes
                                SET  
    	                        ID_matricula = '" & ID_matricula & "',
    	                        Ap_paterno = '" & Ap_paterno & "',
    	                        Ap_materno ='" & Ap_materno & "',
    	                        Nombres = '" & Nombres & "',
    	                        Registro_Patronal = '" & Registro_Patronal & "',
    	                        Dig_Verificador_RP = '" & Dig_Verificador_RP & "',
    	                        No_IMSS = '" & No_IMSS & "',
    	                        Dig_Verif_IMSS = '" & Dig_Verif_IMSS & "',
    	                        Salario_Base =  " & Salario_Base & " ,
    	                        Tipo_Trabajador =  " & Tipo_Trabajador & " ,
    	                        Tipo_Salario =  " & Tipo_Salario & " ,
    	                        Sem_Jornada_Reducida =  " & Sem_Jornada_Reducida & " ,
    	                        Unidad_Medicina_Familiar = '" & Unidad_Medicina_Familiar & "',
                                Guia = '" & Guia & "',
    	                        CURP = '" & CURP & "',
    	                        Fecha_alta =  " & IIf(Fecha_alta = "", "NULL", Eventos.Sql_hoy(Fecha_alta)) & " ,
    	                        Fecha_Baja = " & IIf(Fecha_Baja = "", "NULL", Eventos.Sql_hoy(Fecha_Baja)) & "

    	 where Id_Persona_Cliente = " & Id_Persona_Cliente & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Id_Persona_Cliente As Integer, ByVal ID_matricula As Integer, ByVal Ap_paterno As String,
                               ByVal Ap_materno As String, ByVal Nombres As String, ByVal Registro_Patronal As String,
                               ByVal Dig_Verificador_RP As String, ByVal No_IMSS As String, ByVal Dig_Verif_IMSS As String,
                               ByVal Salario_Base As Decimal, ByVal Tipo_Trabajador As Integer, ByVal Tipo_Salario As Integer,
                                ByVal Sem_Jornada_Reducida As Integer, ByVal Unidad_Medicina_Familiar As String, ByVal Guia As String,
                                 ByVal CURP As String, ByVal Fecha_alta As String)
        Dim sql As String = "INSERT INTO dbo.Personal_Clientes
    	                        (
    	                        ID_matricula,
    	                        Ap_paterno,
    	                        Ap_materno,
    	                        Nombres,
    	                        Registro_Patronal,
    	                        Dig_Verificador_RP,
    	                        No_IMSS,
    	                        Dig_Verif_IMSS,
    	                        Salario_Base,
    	                        Tipo_Trabajador,
    	                        Tipo_Salario,
    	                        Sem_Jornada_Reducida,
    	                        Unidad_Medicina_Familiar,
     	                        Guia,
    	                        CURP,
    	                        Id_Empresa,
    	                        Fecha_alta
    	                            )
                                VALUES 
                                	(

                                	'" & ID_matricula & "',
                                	'" & Ap_paterno & "',
                                	'" & Ap_materno & "',
                                	'" & Nombres & "',
                                	'" & Registro_Patronal & "',
                                	'" & Dig_Verificador_RP & "',
                                	'" & No_IMSS & "',
                                	'" & Dig_Verif_IMSS & "',
                                	" & Salario_Base & ",
                                	" & Tipo_Trabajador & ",
                                	" & Tipo_Salario & ",
                                	" & Sem_Jornada_Reducida & ",
                                	" & Unidad_Medicina_Familiar & ",
                                	'" & Guia & "',
                                    '" & CURP & "',
                                	" & Me.lstCliente.SelectItem & ",
                                	" & IIf(Fecha_alta = "", "NULL", Eventos.Sql_hoy(Fecha_alta)) & "
                                	)"


        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub

    Private Sub CmdMovimientos_Click(sender As Object, e As EventArgs) Handles CmdMovimientos.Click
        Eventos.Abrir_form(Altas_Bajas_Modificaciones)
    End Sub

    Private Sub TablaImportar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellClick
        Try


            Dim columna As Integer = Me.TablaImportar.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaImportar.Columns.Item(Me.TablaImportar.CurrentCell.ColumnIndex).Name.ToString
            If Me.Tabla.Rows.Count > 0 Then
                Me.Tabla.Rows.Clear()
            End If

            Select Case Nombre
                Case "TipoSalario"
                    Me.Tabla.Rows.Add("0", "Salario Fijo")
                    Me.Tabla.Rows.Add("1", "Salario Variable")
                    Me.Tabla.Rows.Add("2", "Salario Mixto")
                    Me.LstTexto.Cargar(" SELECT  0, 'Salario Fijo'  UNION SELECT  1 , 'Salario Variable' UNION SELECT 2 , 'Salario Mixto'")
                    Me.LstTexto.SelectText = ""
                Case "TipoTrabajador"
                    Me.Tabla.Rows.Add("1", "Trabajador Permanente")
                    Me.Tabla.Rows.Add("2", "Trabajador Eventual Ciudad")
                    Me.Tabla.Rows.Add("3", "Trabajador Eventual Construccion")
                    Me.Tabla.Rows.Add("4", "Trabajador Eventual Campo")
                    Me.LstTexto.Cargar(" SELECT  1, 'Trabajador Permanente'  UNION SELECT  2 , 'Trabajador Eventual Ciudad' UNION SELECT 3 , 'Trabajador Eventual Construccion' UNION SELECT 4 , 'Trabajador Eventual Campo'")
                    Me.LstTexto.SelectText = ""
                Case "SemJornadaReducida"
                    Me.Tabla.Rows.Add("0", "Jornada Normal")
                    Me.Tabla.Rows.Add("1", "Un Dia")
                    Me.Tabla.Rows.Add("2", "Dos Dias")
                    Me.Tabla.Rows.Add("3", "Tres Dias")
                    Me.Tabla.Rows.Add("4", "Cuatro Dias")
                    Me.Tabla.Rows.Add("5", "Cinco Dias")
                    Me.Tabla.Rows.Add("6", "Jornada Reducida")
                    Me.LstTexto.Cargar(" SELECT  0, 'Jornada Normal'  UNION SELECT  1 , 'Un Dia' UNION SELECT 2 ,'Dos Dias' UNION SELECT 3 , 'Tres Dias' UNION SELECT 4 , 'Cuatro Dias' UNION SELECT 5 , 'Cinco Dias' UNION SELECT 6 , 'Jornada Reducida'")
                    Me.LstTexto.SelectText = ""
                Case "Guia"
                    Me.Tabla.Rows.Add("371", "Revisión por Art. 18")
                    Me.Tabla.Rows.Add("373", "Visita Integral Art. 46")
                    Me.Tabla.Rows.Add("374", "Revisión de Gabinete Art. 48")
                    Me.Tabla.Rows.Add("375", "Revisión por Art. 12 A")
                    Me.Tabla.Rows.Add("376", "Visita Específica Art. 46")
                    Me.Tabla.Rows.Add("397", "Revisión Ágil Art. 17")
                    Me.Tabla.Rows.Add("400", "Dispositivos Magnéticos (Reingresos, Modificaciones de Salario y Bajas)")
                    Me.Tabla.Rows.Add("405", "Corrección por Invitación, Corrección Espontánea, SATICA o SATICB.")
                    Me.Tabla.Rows.Add("406", "Programa de Dictamen (Obligado y Voluntario), Procedimiento de Revisión Interna (RO y RV)")
                    Me.LstTexto.Cargar(" SELECT  371, 'Revisión por Art. 18'  UNION SELECT  373 , 'Visita Integral Art. 46' UNION SELECT 374 ,'Revisión de Gabinete Art. 48' 
                            UNION SELECT 375 , 'Revisión por Art. 12 A' UNION SELECT 376 , 'Visita Específica Art. 46' UNION SELECT 397 , 'Revisión Ágil Art. 17' 
                            UNION SELECT 400 , 'Reingresos, Modificaciones de Salario y Bajas' 
                            UNION SELECT 405 , 'Corrección por Invitación, Corrección Espontánea, SATICA o SATICB' 
                            UNION SELECT 406 , 'Programa de Dictamen (Obligado y Voluntario), Procedimiento de Revisión Interna (RO y RV)'")
                    Me.LstTexto.SelectText = ""
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmdAsignaDelegacion_Click(sender As Object, e As EventArgs) Handles CmdAsignaDelegacion.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim sql As String = ""
        If Me.LstDelegacion.SelectText <> "" And Me.LstSubDelegacion.SelectText <> "" Then
            If tiene = True Then
                sql = "  UPDATE dbo.Clientes_Delegaciones SET 	Id_Sub_Delegacion_IMSS = " & Me.LstSubDelegacion.SelectItem & " WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Else
                sql = "INSERT INTO dbo.Clientes_Delegaciones ( Id_Empresa, Id_Sub_Delegacion_IMSS ) VALUES 	(" & Me.lstCliente.SelectItem & "," & Me.LstSubDelegacion.SelectItem & ")"
            End If
            If Eventos.Comando_sql(sql) > 0 Then
                RadMessageBox.Show("Informacion Guardada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Cargar_clientes()
            Else
                RadMessageBox.Show("No se pudo guardar la informacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

            End If
        End If
    End Sub

    Private Sub LstDelegacion_cambio_item(value As String, texto As String) Handles LstDelegacion.Cambio_item
        If activo = False Then
            Me.LstSubDelegacion.Cargar("SELECT Sub_Delegaciones_IMSS.Id_Sub_Delegacion_IMSS, Sub_Delegaciones_IMSS.Sub_Delegacion   
                                    FROM Sub_Delegaciones_IMSS 
                                    INNER JOIN Delegaciones_IMSS ON Delegaciones_IMSS.Id_Delegacion_IMSS =Sub_Delegaciones_IMSS.Id_Delegacion_IMSS 
                                    WHERE Delegaciones_IMSS.Id_Delegacion_IMSS =" & value & "")
            Me.LstSubDelegacion.SelectText = ""
        End If
    End Sub
    Public Function Calcula_Guia()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Guia As String = ""

        If clave <> "" Then
            Guia = clave & "400"
        Else
            RadMessageBox.Show("Selecciona parametros de Delegacion y Sub-Delegacion a la Empresa " & Me.lstCliente.SelectText & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
        Return Guia
    End Function

    Private Sub LstTexto_Enters() Handles LstTexto.Enters
        Try

            If Me.TablaImportar.Rows.Count > 0 Then
                For Each Fila As DataGridViewRow In TablaImportar.Rows
                    If Fila.Cells(TipoTrabajador.Index).Selected = True Then
                        Try
                            Fila.Cells(TipoTrabajador.Index).Value = Trim(Me.LstTexto.SelectItem)
                        Catch ex As Exception

                        End Try
                    ElseIf Fila.Cells(TipoSalario.Index).Selected = True Then
                        Try
                            Fila.Cells(TipoSalario.Index).Value = Trim(Me.LstTexto.SelectItem)
                        Catch ex As Exception

                        End Try
                    ElseIf Fila.Cells(SemJornadaReducida.Index).Selected = True Then
                        Try
                            Fila.Cells(SemJornadaReducida.Index).Value = Trim(Me.LstTexto.SelectItem)
                        Catch ex As Exception

                        End Try
                    ElseIf Fila.Cells(Guia.Index).Selected = True Then
                        Try
                            Fila.Cells(Guia.Index).Value = Calcula_Guia(Trim(Me.LstTexto.SelectItem))
                        Catch ex As Exception

                        End Try
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function Calcula_Guia(ByVal Gu As String)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Guia As String = ""

        If clave <> "" Then
            Guia = clave & Trim(Gu)
        Else
            RadMessageBox.Show("Selecciona parametros de Delegacion y Sub-Delegacion a la Empresa " & Me.lstCliente.SelectText & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
        Return Guia
    End Function
End Class