Public Class ListaNegra
    Dim Activo As Boolean
    Dim dt As New DataTable()
    Private Sub ListaNegra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Listas()
        Buscar()
        Activo = False
    End Sub
    Private Sub Listas()
        Me.lstClientes.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstClientes.SelectItem = My.Forms.Inicio.Clt
    End Sub

    Private Sub CmdActualizar_Click(sender As Object, e As EventArgs) Handles CmdActualizar.Click
        Buscar()
    End Sub
    Private Sub Buscar()
        If Me.Tabla.Columns.Count > 0 Then
            Me.Tabla.Columns.Clear()
        End If
        If dt.Rows.Count > 0 Then
            dt.Rows.Clear()
        End If
        Dim Sql As String = " Select Id_LN, Contribuyente ,RFC,FechaV as [Fecha Lista Negra] From ListaNegra  WHERE Id_Empresa = " & Me.lstClientes.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.DataSource = ds.Tables(0).DefaultView
            Me.Tabla.Columns(0).Visible = False

        End If
    End Sub

    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdNuevo_Click(sender As Object, e As EventArgs) Handles CmdNuevo.Click
        If Me.Tabla.Columns.Count > 0 Then
            Me.Tabla.Columns.Clear()
        End If
        Me.Tabla.Rows.Add()
    End Sub
End Class
