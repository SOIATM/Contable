Public Class Tablas

    Inherits DataGridView
    Sub New()
        Me.BackColor = Color.WhiteSmoke
        Me.BorderStyle = BorderStyle.Fixed3D
        Me.RowsDefaultCellStyle.BackColor = Color.CadetBlue
        Me.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        Me.RowsDefaultCellStyle.SelectionBackColor = Color.Crimson
        Me.GridColor = Color.WhiteSmoke
        Me.Dock = Dock.Fill
        Me.EnableHeadersVisualStyles = True
        Me.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue
        Me.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke
        Me.ColumnHeadersDefaultCellStyle.Font = New Font("Franklin Gothic Medium", 10, FontStyle.Regular)
        Me.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.ColumnHeadersBorderStyle = ColumnHeadersBorderStyle.Single
        Me.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.ColumnHeadersHeight = 30
        Me.RowHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
        Me.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Crimson
        Me.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        Me.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        Me.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.AllowUserToAddRows = False
        Me.ScrollBars = ScrollBars.Both
    End Sub

End Class
