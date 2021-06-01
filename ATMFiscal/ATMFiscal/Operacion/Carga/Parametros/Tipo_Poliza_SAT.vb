Imports Telerik.WinControls
Public Class Tipo_Poliza_SAT
    Dim activo As Boolean
    Private Sub Tipo_Poliza_SAT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Me.lstCliente_Enters()
        activo = False
    End Sub

    Private Sub lstCliente_Enters() Handles lstCliente.Enters
        If Me.lstCliente.SelectText <> "" Then
            Me.Tabla_detallePolizasSat.SqlSelect = "SELECT 	Id_Tipo_Pol_Sat,	Clave,	Nombre,	Id_Empresa FROM dbo.Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " order by Clave"
            Me.Tabla_detallePolizasSat.Cargar()
            Me.Tabla_detallePolizasSat.Tabla.Columns(0).Visible = False
            Me.Tabla_detallePolizasSat.Tabla.Columns(3).Visible = False
        Else
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If

    End Sub

    Private Sub Tabla_detallePolizasSat_cmd_Editar(clave As String) Handles Tabla_detallePolizasSat.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, Me.Tabla_detallePolizasSat.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim descripcion As String = InputBox("Teclea el Nombre del Banco:", Eventos.titulo_app, Me.Tabla_detallePolizasSat.Registro_columna(2))
        If descripcion = "" Then
            RadMessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Tipo_poliza) + '.- ' + Tipo FROM Tipo_Poliza ")
        Dim actividad(,) As String
        ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            actividad(0, i) = ds.Tables(0).Rows(i)(0)
            Debug.Print(ds.Tables(0).Rows(i)(0))
            actividad(1, i) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona la Clave para polizas del Sistema:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = actividad
            .ShowDialog()
            actividad = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim descrip As String = ""
        For i As Integer = 0 To actividad.GetLength(1)
            If actividad(1, i) = "1" Then
                descrip = actividad(0, i).ToString.Substring(0, 1)
                Exit For
            End If
        Next


        Dim sql As String = "UPDATE dbo.Tipos_Poliza_Sat"
        sql &= " SET   "
        sql &= "	Clave = '" & claves & "'," '@Clave_tipo
        sql &= "	Id_Tipo_poliza = " & descrip & "," '@Clave_tipo
        sql &= "	Nombre = '" & descripcion & "' " '@tipo
        sql &= " where Id_Tipo_Pol_Sat= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tipos_Poliza_Sat_E", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detallePolizasSat_cmd_eliminar(clave As String) Handles Tabla_detallePolizasSat.Cmd_eliminar
        If RadMessageBox.Show("Realmente deseas eliminar el Tipo: " & Me.Tabla_detallePolizasSat.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Tipos_Poliza_Sat where Id_Tipo_Pol_Sat=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Tipos_Poliza_Sat_D", "Delete from dbo.Tipos_Poliza_Sat where Id_Tipo_Pol_Sat= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detallePolizasSat_cmd_Nuevo() Handles Tabla_detallePolizasSat.Cmd_Nuevo

        If Me.lstCliente.SelectText <> "" Then
            'MasivosTiposPoliza.Cliente = Me.lstCliente.SelectItem
            'Eventos.Abrir_form(MasivosTiposPoliza)
        Else
            RadMessageBox.Show("Debes seleccionar una Empresa primero...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub lstCliente_CursorChanged(sender As Object, e As EventArgs) Handles lstCliente.CursorChanged
        If activo = False Then

            If Me.lstCliente.SelectText <> "" Then
                Me.Tabla_detallePolizasSat.SqlSelect = "SELECT 	Id_Tipo_Pol_Sat,	Clave,	Nombre,	Id_Empresa FROM dbo.Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & ""
                Me.Tabla_detallePolizasSat.Cargar()
                Me.Tabla_detallePolizasSat.Tabla.Columns(0).Visible = False
            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If

        End If
    End Sub

    Private Sub Tabla_detallePolizasSat_Cerrar() Handles Tabla_detallePolizasSat.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Tipo de Polizas Sat")
    'End Sub


End Class