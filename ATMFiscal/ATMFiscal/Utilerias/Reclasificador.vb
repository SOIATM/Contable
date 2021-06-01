Imports Telerik.WinControls
Public Class Reclasificador
    Private Sub Reclasificador_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
    End Sub
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Me.Lstctainicial.Cargar(" SELECT Catalogo_de_Cuentas.Cuenta, rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) + '-'+  Descripcion AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & "   order by Alias ")
        Me.Lstctainicial.SelectText = ""

        Me.Lstctafinal.Cargar(" SELECT Catalogo_de_Cuentas.Cuenta, rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) + '-'+  Descripcion AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & "   order by Alias ")
        Me.Lstctafinal.SelectText = ""

    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas cambiar los movimientos de la cuenta " & Me.Lstctainicial.SelectText & " a la cuenta " & Me.Lstctafinal.SelectText & "  ?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = "UPDATE dbo.Detalle_Polizas SET Cuenta = '" & Me.Lstctafinal.SelectItem.ToString().Trim() & "' WHERE ID_poliza IN (SELECT Id_poliza FROM Polizas WHERE Id_Empresa =  " & Me.lstCliente.SelectItem & " AND ( polizas.Fecha >= " & Eventos.Sql_hoy(Me.DtInicio.Value) & " AND polizas.Fecha <= " & Eventos.Sql_hoy(Dtfin.Value) & ")) AND Cuenta = '" & Me.Lstctainicial.SelectItem.ToString().Trim() & "'"
            If Eventos.Comando_sql(sql) > 0 Then

            End If
        Else
            Exit Sub
        End If
        Me.Close()
    End Sub
End Class
