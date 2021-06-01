Imports Telerik.WinControls
Public Class LiberarParcialidades

    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Dim Dato As DataSet
    Private Sub LiberarParcialidades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.Tabla)
        If Permiso(Me.Tag.ToString) Then
            Me.lstCliente.Cargar(" SELECT DISTINCT Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
            Me.lstCliente.SelectItem = 1
            Me.LstUUIDS.Cargar("  SELECT DISTINCT  UUID_Relacion,UUID_Relacion
                                            FROM     Parcialidades 
                                            WHERE  (Parcialidades.Id_Empresa = " & Inicio.Clt & ")")
            Me.LstUUIDS.SelectText = ""
        Else
            RadMessageBox.Show("No tienes permiso para modificar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)


            Me.Cmd_Procesar.Enabled = False

            Me.lstCliente.Enabled = False
        End If
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        SP2.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub

    Private Sub ChkTodos_CheckedChanged(sender As Object, e As EventArgs) Handles ChkTodos.CheckedChanged
        If Me.ChkTodos.Checked = True Then
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                Me.Tabla.Item(Seleccion.Index, i).Value = True
            Next
        Else
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                Me.Tabla.Item(Seleccion.Index, i).Value = False
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Sql As String = "SELECT UUID_Relacion, ID_poliza , Total_Real FROM dbo.Parcialidades WHERE UUID_Relacion ='" & Me.LstUUIDS.SelectText.Trim() & "' AND Id_Empresa= " & Me.lstCliente.SelectItem & " order by Total_Real"
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Dato = ds
        Else
            Me.Tabla.Rows.Clear()
        End If
        SP1.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub

    Private Sub Cargar(ByVal Ds As DataSet)
        Try
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Calculando por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.Rows.Count
            For j As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                Me.Tabla.Item(UUID.Index, j).Value = Ds.Tables(0).Rows(j)("UUID_Relacion").ToString.Trim()
                Me.Tabla.Item(Poliza.Index, j).Value = Ds.Tables(0).Rows(j)("ID_poliza").ToString.Trim()
                Me.Tabla.Item(TotalReal.Index, j).Value = Ds.Tables(0).Rows(j)("Total_Real")
                frm.Barra.Value = j
            Next
            frm.Close()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Cargar(Dato)
    End Sub

    Private Sub SP2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP2.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        If Me.lstCliente.SelectText <> "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If radMessageBox.Show("La empresa " & Me.lstCliente.SelectText & " es correcta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Text = "Liberando por favor espere..."
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla.Rows.Count
                For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                    If Me.Tabla.Item(Seleccion.Index, i).Value = True Then
                        Eventos.LiberarCargaPagos(Me.Tabla.Item(UUID.Index, i).Value, Me.lstCliente.SelectItem, Me.Tabla.Item(Poliza.Index, i).Value)
                    End If
                    frm.Barra.Value = i
                Next
                frm.Close()
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                radMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, radMessageIcon.info)
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            radMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, radMessageIcon.info)
        End If
    End Sub

    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        Me.Close()
    End Sub
End Class