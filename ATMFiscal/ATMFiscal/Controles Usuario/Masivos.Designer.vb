<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Masivos
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Masivos))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdEditar = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtregistros = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblselect = New System.Windows.Forms.Label()
        Me.Tabla = New ATMFiscal.Tablas()
        Me.Selec = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CmdNuevo = New Telerik.WinControls.UI.RadButton()
        CType(Me.CmdEditar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdEditar
        '
        Me.CmdEditar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdEditar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEditar.Location = New System.Drawing.Point(87, 12)
        Me.CmdEditar.Name = "CmdEditar"
        Me.CmdEditar.Size = New System.Drawing.Size(67, 67)
        Me.CmdEditar.TabIndex = 4
        Me.CmdEditar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEditar.ThemeName = "Aqua"
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = CType(resources.GetObject("CmdCerrar.Image"), System.Drawing.Image)
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(14, 12)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(67, 67)
        Me.CmdCerrar.TabIndex = 3
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CmdNuevo)
        Me.Panel1.Controls.Add(Me.txtregistros)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.lblselect)
        Me.Panel1.Controls.Add(Me.CmdCerrar)
        Me.Panel1.Controls.Add(Me.CmdEditar)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1022, 101)
        Me.Panel1.TabIndex = 5
        '
        'txtregistros
        '
        Me.txtregistros.AutoSize = True
        Me.txtregistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtregistros.Location = New System.Drawing.Point(384, 35)
        Me.txtregistros.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtregistros.Name = "txtregistros"
        Me.txtregistros.Size = New System.Drawing.Size(16, 18)
        Me.txtregistros.TabIndex = 10
        Me.txtregistros.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(304, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 18)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Registros:"
        '
        'lblselect
        '
        Me.lblselect.AutoSize = True
        Me.lblselect.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblselect.Location = New System.Drawing.Point(423, 35)
        Me.lblselect.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblselect.Name = "lblselect"
        Me.lblselect.Size = New System.Drawing.Size(43, 18)
        Me.lblselect.TabIndex = 8
        Me.lblselect.Text = "Select"
        Me.lblselect.Visible = False
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.Tabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Tabla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.Tabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.CadetBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selec})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.EnableHeadersVisualStyles = False
        Me.Tabla.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!)
        Me.Tabla.GridColor = System.Drawing.Color.CadetBlue
        Me.Tabla.Location = New System.Drawing.Point(0, 101)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Crimson
        Me.Tabla.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1022, 473)
        Me.Tabla.TabIndex = 6
        '
        'Selec
        '
        Me.Selec.HeaderText = "Asignar"
        Me.Selec.Name = "Selec"
        Me.Selec.Width = 70
        '
        'CmdNuevo
        '
        Me.CmdNuevo.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevo.Location = New System.Drawing.Point(160, 12)
        Me.CmdNuevo.Name = "CmdNuevo"
        Me.CmdNuevo.Size = New System.Drawing.Size(67, 67)
        Me.CmdNuevo.TabIndex = 11
        Me.CmdNuevo.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevo.ThemeName = "Aqua"
        '
        'Masivos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Masivos"
        Me.Size = New System.Drawing.Size(1022, 574)
        CType(Me.CmdEditar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Ayuda As ToolTip
    Friend WithEvents CmdEditar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtregistros As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblselect As Label
    Friend WithEvents Tabla As Tablas
    Friend WithEvents Selec As DataGridViewCheckBoxColumn
    Friend WithEvents CmdNuevo As Telerik.WinControls.UI.RadButton
End Class
