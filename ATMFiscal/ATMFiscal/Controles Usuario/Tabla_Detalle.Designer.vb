<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class tabla_detalle
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(tabla_detalle))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.txtregistros = New System.Windows.Forms.Label()
        Me.lblSqlInsert = New System.Windows.Forms.Label()
        Me.LblSqlSelect = New System.Windows.Forms.Label()
        Me.lblselect = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmdRefrescar = New Telerik.WinControls.UI.RadButton()
        Me.CmdExcel = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminar = New Telerik.WinControls.UI.RadButton()
        Me.CmdEditar = New Telerik.WinControls.UI.RadButton()
        Me.CmdAgregar = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Tabla = New ATMFiscal.Tablas()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdRefrescar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEditar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.txtregistros)
        Me.RadPanel1.Controls.Add(Me.lblSqlInsert)
        Me.RadPanel1.Controls.Add(Me.LblSqlSelect)
        Me.RadPanel1.Controls.Add(Me.lblselect)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.CmdRefrescar)
        Me.RadPanel1.Controls.Add(Me.CmdExcel)
        Me.RadPanel1.Controls.Add(Me.CmdEliminar)
        Me.RadPanel1.Controls.Add(Me.CmdEditar)
        Me.RadPanel1.Controls.Add(Me.CmdAgregar)
        Me.RadPanel1.Controls.Add(Me.CmdCerrar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1624, 115)
        Me.RadPanel1.TabIndex = 1
        Me.RadPanel1.ThemeName = "Material"
        '
        'txtregistros
        '
        Me.txtregistros.AutoSize = True
        Me.txtregistros.Location = New System.Drawing.Point(171, 84)
        Me.txtregistros.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtregistros.Name = "txtregistros"
        Me.txtregistros.Size = New System.Drawing.Size(20, 22)
        Me.txtregistros.TabIndex = 28
        Me.txtregistros.Text = "0"
        '
        'lblSqlInsert
        '
        Me.lblSqlInsert.AutoSize = True
        Me.lblSqlInsert.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSqlInsert.Location = New System.Drawing.Point(1391, 28)
        Me.lblSqlInsert.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSqlInsert.Name = "lblSqlInsert"
        Me.lblSqlInsert.Size = New System.Drawing.Size(41, 18)
        Me.lblSqlInsert.TabIndex = 27
        Me.lblSqlInsert.Text = "insert"
        Me.lblSqlInsert.Visible = False
        '
        'LblSqlSelect
        '
        Me.LblSqlSelect.AutoSize = True
        Me.LblSqlSelect.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSqlSelect.Location = New System.Drawing.Point(1446, 28)
        Me.LblSqlSelect.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblSqlSelect.Name = "LblSqlSelect"
        Me.LblSqlSelect.Size = New System.Drawing.Size(42, 18)
        Me.LblSqlSelect.TabIndex = 26
        Me.LblSqlSelect.Text = "select"
        Me.LblSqlSelect.Visible = False
        '
        'lblselect
        '
        Me.lblselect.Location = New System.Drawing.Point(1394, 49)
        Me.lblselect.Margin = New System.Windows.Forms.Padding(4)
        Me.lblselect.Name = "lblselect"
        Me.lblselect.Size = New System.Drawing.Size(115, 22)
        Me.lblselect.TabIndex = 25
        Me.lblselect.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1349, 35)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 21)
        Me.Label5.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 85)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 21)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Total de registros:"
        '
        'CmdRefrescar
        '
        Me.CmdRefrescar.Image = Global.ATMFiscal.My.Resources.Resources.Actualizar
        Me.CmdRefrescar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdRefrescar.Location = New System.Drawing.Point(77, 11)
        Me.CmdRefrescar.Name = "CmdRefrescar"
        Me.CmdRefrescar.Size = New System.Drawing.Size(67, 67)
        Me.CmdRefrescar.TabIndex = 3
        Me.CmdRefrescar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdRefrescar.ThemeName = "Aqua"
        '
        'CmdExcel
        '
        Me.CmdExcel.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExcel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExcel.Location = New System.Drawing.Point(361, 11)
        Me.CmdExcel.Name = "CmdExcel"
        Me.CmdExcel.Size = New System.Drawing.Size(67, 67)
        Me.CmdExcel.TabIndex = 2
        Me.CmdExcel.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExcel.ThemeName = "Aqua"
        '
        'CmdEliminar
        '
        Me.CmdEliminar.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminar.Location = New System.Drawing.Point(290, 11)
        Me.CmdEliminar.Name = "CmdEliminar"
        Me.CmdEliminar.Size = New System.Drawing.Size(67, 67)
        Me.CmdEliminar.TabIndex = 2
        Me.CmdEliminar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminar.ThemeName = "Aqua"
        '
        'CmdEditar
        '
        Me.CmdEditar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdEditar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEditar.Location = New System.Drawing.Point(219, 11)
        Me.CmdEditar.Name = "CmdEditar"
        Me.CmdEditar.Size = New System.Drawing.Size(67, 67)
        Me.CmdEditar.TabIndex = 2
        Me.CmdEditar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEditar.ThemeName = "Aqua"
        '
        'CmdAgregar
        '
        Me.CmdAgregar.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdAgregar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAgregar.Location = New System.Drawing.Point(148, 11)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.Size = New System.Drawing.Size(67, 67)
        Me.CmdAgregar.TabIndex = 1
        Me.CmdAgregar.TabStop = False
        Me.CmdAgregar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdAgregar.ThemeName = "Aqua"
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = CType(resources.GetObject("CmdCerrar.Image"), System.Drawing.Image)
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(6, 11)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(67, 67)
        Me.CmdCerrar.TabIndex = 0
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Tabla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.Tabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.CadetBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.EnableHeadersVisualStyles = False
        Me.Tabla.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular)
        Me.Tabla.GridColor = System.Drawing.Color.CadetBlue
        Me.Tabla.Location = New System.Drawing.Point(0, 115)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Crimson
        Me.Tabla.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.Tabla.Size = New System.Drawing.Size(1624, 249)
        Me.Tabla.TabIndex = 4
        '
        'tabla_detalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "tabla_detalle"
        Me.Size = New System.Drawing.Size(1624, 364)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdRefrescar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEditar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAgregar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents lblSqlInsert As Label
    Friend WithEvents LblSqlSelect As Label
    Friend WithEvents lblselect As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CmdRefrescar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEditar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdAgregar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtregistros As Label
    Friend WithEvents Tabla As Tablas
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
End Class
