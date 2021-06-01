Imports Telerik.WinControls
Public Class MasivoCancelacionesIVA
    Public Cliente As Integer
    Private Sub MasivoCancelacionesIVA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar()
    End Sub
    Private Sub Cargar()
        Me.MasivosCancelacionesIVA.SqlSelect = "SELECT Catalogo_de_Cuentas.Cuenta, Catalogo_de_Cuentas.Descripcion, Catalogo_de_Cuentas.Id_cat_Cuentas, CancelacionesIVA.Id_Cancelacion,CancelacionesIVA.Abono FROM     Catalogo_de_Cuentas LEFT OUTER JOIN CancelacionesIVA ON Catalogo_de_Cuentas.Id_cat_Cuentas = CancelacionesIVA.Id_cat_Cuentas WHERE  
(Catalogo_de_Cuentas.Id_Empresa =   " & Cliente & ") AND (Cuenta IN (1180000100010000, 1180000100020000, 1180000100030000, 1180000100040000, 1180000100050000, 1180000100060000, 1180000100070000, 1180000100080000, 1180000100090000, 1180000100100001, 
                  1180000100100002,1180000100110000,1180000100120000,1180000100130000,1180000100140000,1180000100150000,1180000100160000,1180000100170000,1180000100180000,1180000100190000,1180000100200000,1180000100200001,1180000100200002,
                   1180000200000000,  2080000100010000, 2080000100020000, 2080000100030000, 2080000100040000, 2080000100050000, 2080000100060000, 2080000100110000,2080000100120000,2080000100130000,
                   2080000100140000,2080000100150000,2080000100160000, 1130000800010000, 6010001000590000,6020001000590000,6030001000590000, 6120000100010000, 7040002300010000 )) OR
                  (Catalogo_de_Cuentas.Id_Empresa =   " & Cliente & ") AND (  Nivel1 = '1130' AND Nivel2 = '0001' AND Nivel3 >0) and (  Nivel1 = '2130' AND Nivel2 = '0001' AND Nivel3 >0)order by Cuenta "

        Me.MasivosCancelacionesIVA.Cargar()
        For i As Integer = 0 To Me.MasivosCancelacionesIVA.Tabla.Rows.Count - 1
            VeriF(Me.MasivosCancelacionesIVA.Tabla.Item(3, i).Value, i)
        Next

        Me.MasivosCancelacionesIVA.Tabla.Columns(3).Visible = False
        Me.MasivosCancelacionesIVA.Tabla.Columns(4).Visible = False

    End Sub
    Private Sub VeriF(ByVal clave As String, ByVal i As Integer)
        Dim sql As String = "SELECT CancelacionesIVA.Id_cat_Cuentas FROM CancelacionesIVA INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas  = CancelacionesIVA.Id_cat_Cuentas 
                                WHERE CancelacionesIVA.Id_Empresa =  " & Cliente & " and  CancelacionesIVA.Id_cat_Cuentas = " & clave & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.MasivosCancelacionesIVA.Tabla.Item(0, i).Value = True
        Else
            Me.MasivosCancelacionesIVA.Tabla.Item(0, i).Value = False
        End If
    End Sub
    Private Sub MasivosCancelacionesIVA_Cerrar() Handles MasivosCancelacionesIVA.Cerrar
        Me.Close()
    End Sub
    Private Sub MasivosCancelacionesIVA_Cmd_Editar(clave As String) Handles MasivosCancelacionesIVA.Cmd_Editar
        If Eventos.Comando_sql("Delete from dbo.CancelacionesIVA where Id_Empresa=" & Cliente & " ") > 0 Then
        End If
        For i As Integer = 0 To Me.MasivosCancelacionesIVA.Tabla.Rows.Count - 1
            If Me.MasivosCancelacionesIVA.Tabla.Item(0, i).Value = True Then

                Dim sql As String = "  INSERT INTO dbo.CancelacionesIVA"
                sql &= "("
                sql &= " 	Id_Empresa ,"
                sql &= " 	Abono ,"
                sql &= " 	Id_cat_Cuentas "
                sql &= " 	)"
                sql &= " VALUES "
                sql &= "("
                sql &= " 	" & Cliente & "," '
                sql &= " 	" & Eventos.Bool2(IIf(IsDBNull(Me.MasivosCancelacionesIVA.Tabla.Item(5, i).Value) = True, 0, Me.MasivosCancelacionesIVA.Tabla.Item(5, i).Value)) & "," '
                sql &= " 	" & Trim(Me.MasivosCancelacionesIVA.Tabla.Item(3, i).Value) & "" '
                sql &= " 	)"
                If Eventos.Comando_sql(sql) = 1 Then
                    'MessageBox.Show("Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Eventos.Insertar_usuariol("CancelacionesIVA", sql)
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            Else

            End If

        Next
        CancelacionIva.Tabla_detalleCancelaciones.CmdRefrescar.PerformClick()
        Me.Close()
    End Sub

    Private Function VerificaExistencia(ByVal clave As String)
        Dim Hacer As Boolean
        Dim sql As String = "select * from CancelacionesIVA where   Id_cat_Cuentas = '" & Trim(clave) & "' and Id_Empresa = " & Cliente & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Hacer = False
        Else
            Hacer = True
        End If
        Return Hacer
    End Function
End Class
