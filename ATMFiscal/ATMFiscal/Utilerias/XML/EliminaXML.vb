Imports Telerik.WinControls
Public Class EliminaXML
    Private Sub EliminaXML_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
    Private Sub Cargar_Listas()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Permiso(Me.Tag.ToString) Then
            Me.lstCliente.Cargar("SELECT Empresa.Id_Empresa, Empresa.Razon_Social as Razon , Usuarios.Usuario
                                FROM     Empresa INNER JOIN
                                Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario

                                WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%') order by Razon ")
            Me.lstCliente.SelectItem = 1
        Else
            RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

            Me.CmdImportar.Enabled = False
            Me.Cmd_Procesar.Enabled = False

            Me.lstCliente.Enabled = False
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click

        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.LstCFDI.SelectText <> "" Then
                Dim ds As DataSet = Eventos.Obtener_DS("SELECT 	Id_Registro_Xml,	UUID,	Total,	Conceptos FROM dbo.Xml_Sat where UUID ='" & Trim(Me.LstCFDI.SelectText) & "' ")
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
                        Me.TablaImportar.Item(Id.Index, i).Value = Trim(ds.Tables(0).Rows(i)(0))
                        Me.TablaImportar.Item(UUI.Index, i).Value = Trim(ds.Tables(0).Rows(i)(1))
                        Me.TablaImportar.Item(Imp.Index, i).Value = Trim(ds.Tables(0).Rows(i)(2))
                        Me.TablaImportar.Item(Ref.Index, i).Value = ds.Tables(0).Rows(i)(3)
                    Next
                End If
            End If
        Else

        End If
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el XML: " & Me.LstCFDI.SelectText & " de la Empresa " & Me.lstCliente.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
            For i As Integer = 0 To Me.TablaImportar.Rows.Count
                If Me.TablaImportar.Item(Verif.Index, i).Value = True Then
                    Dim sql As String = "Delete from Xml_Sat where Id_Empresa = " & Me.lstCliente.SelectItem & " and Id_Registro_Xml =" & Me.TablaImportar.Item(Id.Index, i).Value & ""
                    If Eventos.Comando_sql(sql) > 0 Then
                        Eventos.Insertar_usuariol("XML_D", sql)
                    Else
                        RadMessageBox.Show("No se pudo eliminar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                        Exit Sub
                    End If

                End If
            Next
            RadMessageBox.Show("Proceso Terminado ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.CmdLimpiar.PerformClick()
        Else
            Exit Sub
        End If
    End Sub


End Class