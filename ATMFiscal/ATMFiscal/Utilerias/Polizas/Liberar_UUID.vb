Imports Telerik.WinControls
Public Class Liberar_UUID
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Limpia()
            Me.lstCliente.SelectText = ""
            Me.LblFiltro.Text = ""
            Me.TxtFiltro.Text = ""
        End If
    End Sub
    Private Sub Limpia()
        Me.TablaImportar.Rows.Clear()
    End Sub
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Limpia()
        Dim sql As String = ""
        If Me.lstCliente.SelectText <> "" Then
            If Me.ChkFacturas.Checked = True Then
                If Me.RadEmitidas.Checked = True Then
                    sql = "SELECT Xml_Sat.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Sat INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Sat.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND xml_sat.Id_Empresa = " & Me.lstCliente.SelectItem & "   AND xml_sat.Emitidas =1 "
                Else
                    sql = "SELECT Xml_Sat.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Sat INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Sat.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND xml_sat.Id_Empresa = " & Me.lstCliente.SelectItem & "   AND xml_sat.Emitidas =0 "
                End If
            Else
                If Me.RadEmitidas.Checked = True Then
                    sql = "SELECT Xml_Complemento.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Complemento INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Complemento.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND Xml_Complemento.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Xml_Complemento.Emitidas =1"
                Else
                    sql = "SELECT Xml_Complemento.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Complemento INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Complemento.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND Xml_Complemento.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Xml_Complemento.Emitidas =0"
                End If

            End If

            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.TablaImportar.Item(UUID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("UUID"))
                    Me.TablaImportar.Item(anio.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_anio"))
                    Me.TablaImportar.Item(Mes.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_mes"))
                    Me.TablaImportar.Item(Num.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Num_Pol"))
                    Me.TablaImportar.Item(Pol.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_poliza"))
                Next
            End If
        End If
    End Sub


    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles CmdEliminar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.lstCliente.SelectText <> "" Then
                If Me.TablaImportar.Item(Verif.Index, i).Value = True Then
                    Eventos.LiberarCarga_UUID(Me.TablaImportar.Item(UUID.Index, i).Value, Me.ChkComplementos.Checked, Me.ChkFacturas.Checked, Me.lstCliente.SelectItem, Eventos.Bool2(Me.RadEmitidas.Checked))
                End If
            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Next
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.CmdImportar.PerformClick()
    End Sub

    Private Sub Liberar_UUID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Permiso(Me.Tag.ToString) Then
            Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
            Me.lstCliente.SelectItem = Inicio.Clt

            ' Me.lstCliente.SelectText = ""
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)

            Me.CmdImportar.Enabled = False
            Me.CmdEliminar.Enabled = False

            Me.lstCliente.Enabled = False
        End If
    End Sub
    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.lstCliente.SelectText <> "" Then
            If Me.LblFiltro.Text <> "" Then
                Me.TablaImportar.Rows.Clear()

                Dim posicion As Integer = 0
                Dim sql As String = ""

                If Me.lstCliente.SelectText <> "" Then
                    If Me.ChkFacturas.Checked = True Then
                        If Me.RadEmitidas.Checked = True Then
                            sql = "SELECT Xml_Sat.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Sat INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Sat.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND xml_sat.Id_Empresa = " & Me.lstCliente.SelectItem & "   AND xml_sat.Emitidas =1 and Xml_Sat.UUID like '%" & Me.TxtFiltro.Text & "%' "
                        Else
                            sql = "SELECT Xml_Sat.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Sat INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Sat.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND xml_sat.Id_Empresa = " & Me.lstCliente.SelectItem & "   AND xml_sat.Emitidas =0 and Xml_Sat.UUID like '%" & Me.TxtFiltro.Text & "%'"
                        End If
                    Else
                        If Me.RadEmitidas.Checked = True Then
                            sql = "SELECT Xml_Complemento.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Complemento INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Complemento.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND Xml_Complemento.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Xml_Complemento.Emitidas =1 and Xml_Complemento.UUID like '%" & Me.TxtFiltro.Text & "%'"
                        Else
                            sql = "SELECT Xml_Complemento.UUID, Polizas.ID_anio , Polizas.ID_mes ,Polizas.Num_Pol, Polizas.ID_poliza 
                        FROM Xml_Complemento INNER JOIN Polizas ON Polizas.ID_poliza = Xml_Complemento.ID_poliza 
                        WHERE Fecha BETWEEN " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND " & Eventos.Sql_hoy(Dtfin.Value) & " AND Xml_Complemento.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Xml_Complemento.Emitidas =0 and Xml_Complemento.UUID like '%" & Me.TxtFiltro.Text & "%'"
                        End If

                    End If

                    Dim ds As DataSet = Eventos.Obtener_DS(sql)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                            Me.TablaImportar.Item(UUID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("UUID"))
                            Me.TablaImportar.Item(anio.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_anio"))
                            Me.TablaImportar.Item(Mes.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_mes"))
                            Me.TablaImportar.Item(Num.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Num_Pol"))
                            Me.TablaImportar.Item(Pol.Index, i).Value = Trim(ds.Tables(0).Rows(i)("ID_poliza"))
                        Next
                    End If
                End If
            End If
        End If
    End Sub
End Class
