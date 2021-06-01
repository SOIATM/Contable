Imports Telerik.WinControls
Public Class Claves_Sat
    Private Sub Claves_Sat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String = "SELECT Tipo_Letra_SAT.Id_Tipo_Letra_Sat, Forma_de_Pago.Clave AS Forma, Uso_CFDI.Clave AS Uso, Tipo_Letra_SAT.Clave
		                  FROM     Tipo_Letra_SAT INNER JOIN
		                  Forma_de_Pago ON Forma_de_Pago.Id_Forma_Pago = Tipo_Letra_SAT.Id_Forma_Pago INNER JOIN
		                  Uso_CFDI ON Uso_CFDI.Id_Uso_CFDI = Tipo_Letra_SAT.Id_Uso_CFDI"
        Me.TablaUsoSAT.SqlSelect = sql
        Me.TablaUsoSAT.Cargar()
        Me.TablaUsoSAT.Tabla.Columns(0).Visible = False
    End Sub

    Private Sub TablaUsoSAT_cmd_Editar(clave As String) Handles TablaUsoSAT.Cmd_Editar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Forma_Pago) + '.- ' + Clave FROM Forma_de_Pago ")
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
            .Texto = "Selecciona la Metodo de pago:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = actividad
            .ShowDialog()
            actividad = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim Forma As String = ""
        For i As Integer = 0 To actividad.GetLength(1)
            If actividad(1, i) = "1" Then
                Forma = Val(actividad(0, i).Substring(0, 1))
                Exit For
            End If
        Next
        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Uso_CFDI) + '.- ' + Clave FROM Uso_CFDI ")

        ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            actividad(0, i) = ds.Tables(0).Rows(i)(0)
            Debug.Print(ds.Tables(0).Rows(i)(0))
            actividad(1, i) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona el Uso del CFDI:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = actividad
            .ShowDialog()
            actividad = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With
        Dim uso As String = ""
        For i As Integer = 0 To actividad.GetLength(1)
            If actividad(1, i) = "1" Then
                uso = Val(actividad(0, i).Substring(0, 1))
                Exit For
            End If
        Next
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, Me.TablaUsoSAT.Registro_columna(3))
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If
        Dim sql As String = "UPDATE dbo.Tipo_Letra_SAT"
        sql &= " SET  "
        sql &= " 	Id_Forma_Pago = " & Forma & ","
        sql &= " 	Id_Uso_CFDI = " & uso & ","
        sql &= " 	Clave = '" & claves & "'"
        sql &= " where Id_Tipo_Letra_Sat= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("claveSat_E", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub TablaUsoSAT_cmd_eliminar(clave As String) Handles TablaUsoSAT.Cmd_eliminar
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el Tipo: " & Me.TablaUsoSAT.Registro_columna(3) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Tipo_Letra_SAT where Id_Tipo_Letra_Sat=" & clave) > 0 Then
                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Eventos.Insertar_usuariol("Tipo_Letra_SAT_D", "Delete from dbo.Tipo_Letra_SAT where Id_Tipo_Letra_Sat= " & clave & "")
            Else
                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If
    End Sub

    Private Sub TablaUsoSAT_cmd_Nuevo() Handles TablaUsoSAT.Cmd_Nuevo
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Forma_Pago) + '.- ' + clave FROM Forma_de_Pago ")
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
            .Texto = "Selecciona la Metodo de pago:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = actividad
            .ShowDialog()
            actividad = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim Forma As String = ""
        For i As Integer = 0 To actividad.GetLength(1)
            If actividad(1, i) = "1" Then
                Forma = Val(actividad(0, i).Substring(0, 1))
                Exit For
            End If
        Next
        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Uso_CFDI) + '.- ' + Clave FROM Uso_CFDI ")

        ReDim actividad(2, ds.Tables(0).Rows.Count + 1)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            actividad(0, i) = ds.Tables(0).Rows(i)(0)
            Debug.Print(ds.Tables(0).Rows(i)(0))
            actividad(1, i) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona el Uso del CFDI:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = actividad
            .ShowDialog()
            actividad = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With
        Dim uso As String = ""
        For i As Integer = 0 To actividad.GetLength(1)
            If actividad(1, i) = "1" Then
                uso = Val(actividad(0, i).Substring(0, 1))
                Exit For
            End If
        Next
        Dim claves As String = InputBox("Teclea la Clave:", Eventos.titulo_app, "")
        If claves = "" Then
            RadMessageBox.Show("Debe colocar una clave valida", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

        Dim sql As String = " INSERT INTO dbo.Tipo_Letra_SAT"
        sql &= " ( "
        sql &= " 	Id_Forma_Pago,"
        sql &= " 	Id_Uso_CFDI,"
        sql &= " 	Clave"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= " 	("
        sql &= " 	" & Forma & "," '@id_forma_pago
        sql &= " 	" & uso & "," '@id_uso_cfdi
        sql &= " 	'" & claves & "'" '@clave
        sql &= " 	)" '
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Eventos.Insertar_usuariol("Tipos_Poliza_Sat_I", sql)
        Else
            RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub

    Private Sub TablaUsoSAT_Cerrar() Handles TablaUsoSAT.Cerrar
        Me.Close()
    End Sub


End Class