Imports Telerik.WinControls
Public Class Tipos_de_Cambio
    Private Sub Tipos_de_Cambio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Tabla_detalleTiposCambio.SqlSelect = "SELECT 	Id_Tipo_Cambio,	Tipo,	Fecha_Aplicacion,	Costo_Pesos FROM dbo.Tipos_de_Cambio"
        Me.Tabla_detalleTiposCambio.Cargar()
        Me.Tabla_detalleTiposCambio.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub Tabla_detalleTiposCambio_cmd_Editar(clave As String) Handles Tabla_detalleTiposCambio.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea El tipo de Cambio: ", Eventos.titulo_app, Me.Tabla_detalleTiposCambio.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Tipo valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim Fecha_I As String = InputBox("Teclea la Fecha Aplicacion : ", Eventos.titulo_app, Today.ToString.Substring(0, 10))
        If Fecha_I = "" Then
            RadMessageBox.Show("Debe colocar un Nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Dim costo As Decimal = InputBox("Teclea el Costo: ", Eventos.titulo_app, 0)
        If Trim(costo) = "" Then
            Exit Sub
        End If

        Dim sql As String = "UPDATE dbo.Tipos_de_Cambio"
        sql &= " SET  "
        sql &= " 	Tipo = '" & claves & "'," ' @tipo
        sql &= " 	Fecha_Aplicacion = " & Eventos.Sql_hoy(Fecha_I) & "," '@fecha_inicio

        sql &= " 	Costo_Pesos = " & costo & "" '@costo
        sql &= " where Id_Tipo_Cambio= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tipos_de_Cambio_E", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub Tabla_detalleTiposCambio_cmd_eliminar(clave As String) Handles Tabla_detalleTiposCambio.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el Tipo: " & Me.Tabla_detalleTiposCambio.Registro_columna(2) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Tipos_de_Cambio where Id_Tipo_Cambio=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Tipos_de_Cambio_D", "Delete from dbo.Tipos_de_Cambio where Id_Tipo_Cambio= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub Tabla_detalleTiposCambio_cmd_Nuevo() Handles Tabla_detalleTiposCambio.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim claves As String = InputBox("Teclea El tipo de Cambio: ", Eventos.titulo_app, Me.Tabla_detalleTiposCambio.Registro_columna(1))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una Tipo valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim Fecha_I As String = InputBox("Teclea la Fecha Aplicacion: ", Eventos.titulo_app, Today.ToString.Substring(0, 10))
        If Fecha_I = "" Then
            RadMessageBox.Show("Debe colocar una Fecha valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Dim costo As Decimal = InputBox("Teclea el Costo en Pesos: ", Eventos.titulo_app, 0)
        If Trim(costo) = "" Then
            Exit Sub
        End If

        Dim sql As String = "INSERT INTO dbo.Tipos_de_Cambio"
        sql &= "("
        sql &= " 	Tipo,"
        sql &= " 	Fecha_Aplicacion,"

        sql &= " 	Costo_Pesos"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= "("
        sql &= " 	'" & claves & "'," '@tipo
        sql &= " 	" & Eventos.Sql_hoy(Fecha_I) & "," '@fecha_inicio

        sql &= " 	" & costo & "" '@costo
        sql &= " 	)"
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tipos_de_Cambio_I", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) 
        'Eventos.Abrir_form(Importar_Tipos_Cambio)
    End Sub

    Private Sub Tabla_detalleTiposCambio_Cerrar() Handles Tabla_detalleTiposCambio.Cerrar
        Me.Close()
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Tipos de cambio")
    'End Sub
End Class