Imports Telerik.WinControls
Public Class MasivosClietesObligaciones
    Public Cliente As Integer
    Dim a = Str(DateTime.Now.Year)
    Dim DatoC As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub MasivosClietesObligaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.Tabla)
        CargarListas()
        Cargar()
        SP1.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub
    Private Sub CargarListas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2004 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Text = a
    End Sub
    Private Sub Segundoplano(ByVal Ds As DataSet)
        For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Me.Tabla.Item(0, i).Value = Ds.Tables(0).Rows(i)("Id_Obligacion")
            Me.Tabla.Item(1, i).Value = Ds.Tables(0).Rows(i)("Descripcion")
        Next
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1
            VeriF(Me.Tabla.Item(0, i).Value, i)
        Next
    End Sub

    Private Sub Cargar()
        Dim Sql As String = "  SELECT  Obligaciones.Id_Obligacion, Obligaciones.Descripcion   FROM     Obligaciones     ORDER BY Obligaciones.Descripcion   "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count

            DatoC = ds
        End If
    End Sub
    Private Sub VeriF(ByVal Valor As String, ByVal i As Integer)
        Dim sql As String
        For Each Columna As DataGridViewColumn In Me.Tabla.Columns
            If Columna.Index > 1 Then
                sql = "SELECT Obligaciones_Cliente.Mes FROM Obligaciones_Cliente INNER JOIN Empresa ON Empresa.Id_empresa  = Obligaciones_Cliente.Id_Empresa 
                                WHERE Obligaciones_Cliente.Id_Obligacion = " & Me.Tabla.Item(0, i).Value & " AND Obligaciones_Cliente.Id_Empresa =  " & Me.lstCliente.SelectItem & " 
                                and Obligaciones_Cliente.Mes = '" & Columna.Name & "' and  Obligaciones_Cliente.Anio = " & Me.comboAño.Text.Trim() & ""
                Dim ds As DataSet = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.Item(Columna.Index, i).Value = True
                Else
                    Try
                        Me.Tabla.Item(Columna.Index, i).Value = False
                    Catch ex As Exception

                    End Try

                End If
            End If
        Next
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub
    Private Sub Limpiar()
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
    End Sub
    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        Limpiar()
        Cargar()
        SP1.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstCliente.SelectText <> "" Then
            If Me.Tabla.Rows.Count > 0 Then
                Dim sql As String = " SELECT * From Obligaciones_Cliente where Id_Empresa = " & Me.lstCliente.SelectItem & " AND  Anio = " & Me.comboAño.Text.Trim() & ""
                Dim ds As DataSet = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    sql = " Delete from Obligaciones_Cliente where Id_Empresa = " & Me.lstCliente.SelectItem & " AND Anio = " & Me.comboAño.Text.Trim() & ""
                    If Comando_sql(sql) > 0 Then
                        For Each fila As DataGridViewRow In Me.Tabla.Rows
                            For Each Columna As DataGridViewColumn In Me.Tabla.Columns
                                If Columna.Index > 1 Then
                                    If Me.Tabla.Item(Columna.Index, fila.Index).Value = True Then
                                        sql = "INSERT INTO dbo.Obligaciones_Cliente
	                                        (Id_Obligacion,	Id_Empresa,	Mes,	Anio	)
                                        VALUES 
	                                        (	" & Me.Tabla.Item(0, fila.Index).Value & ",	" & Me.lstCliente.SelectItem & ",	'" & Columna.Name & "',	" & Me.comboAño.Text.Trim() & "	)"
                                        If Comando_sql(sql) > 0 Then
                                        Else
                                            RadMessageBox.Show("No se genero el parametro de la obligacion " & Me.Tabla.Item(1, fila.Index).Value & " en el mes de " & Columna.Name & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                Else
                    For Each fila As DataGridViewRow In Me.Tabla.Rows
                        For Each Columna As DataGridViewColumn In Me.Tabla.Columns
                            If Columna.Index > 1 Then
                                If Me.Tabla.Item(Columna.Index, fila.Index).Value = True Then
                                    sql = "INSERT INTO dbo.Obligaciones_Cliente
	                                        (Id_Obligacion,	Id_Empresa,	Mes,	Anio	)
                                        VALUES 
	                                        (	" & Me.Tabla.Item(0, fila.Index).Value & ",	" & Me.lstCliente.SelectItem & ",	'" & Columna.Name & "',	" & Me.comboAño.Text.Trim() & "	)"
                                    If Comando_sql(sql) > 0 Then
                                    Else
                                        RadMessageBox.Show("No se genero el parametro de la obligacion " & Me.Tabla.Item(1, fila.Index).Value & " en el mes de " & Columna.Name & "...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            End If
        Else
            RadMessageBox.Show("Se debe Seleccionar un Cliente...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub



    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Segundoplano(DatoC)
        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub

    Private Sub Tabla_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles Tabla.DataError

    End Sub
End Class
