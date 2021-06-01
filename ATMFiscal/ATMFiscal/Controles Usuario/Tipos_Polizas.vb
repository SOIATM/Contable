Imports System.Data.SqlClient
Public Class Tipos_Polizas
    Public Property Datos() As String
        Get
            Return Me.lblselect.Text
        End Get
        Set(ByVal value As String)
            Me.lblselect.Text = value
        End Set
    End Property
    Public Sub Mostrar_Datos()
        Dim con As New SqlConnection("Data Source=" & Trim(My.Forms.Inicio.txtServerDB.Text) & "" & ";Initial Catalog=Contable;Persist Security Info=True;User=ContaP;Password=CpDb2018")
        Try
            con.Open()
            Dim ds As New SqlDataAdapter(Me.lblselect.Text, con)
            Dim ds2 As New DataSet
            ds.Fill(ds2, "TablaDetalle")
            Me.tabla.DataSource = ds2
            Me.tabla.DataMember = "TablaDetalle"
            con.Close()
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos de las Polizas:: " & ex.Message, "Proyecto Contable", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class
