<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ListaNegra
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ListaNegra))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Sup = New Telerik.WinControls.UI.RadPanel()
        Me.Infe = New Telerik.WinControls.UI.RadPanel()
        Me.CmdActualizar = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminar = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevo = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.lstClientes = New ATMFiscal.Listas()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtregistros = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Tabla = New ATMFiscal.Tablas()
        CType(Me.Sup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Sup.SuspendLayout()
        CType(Me.Infe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Infe.SuspendLayout()
        CType(Me.CmdActualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Sup
        '
        Me.Sup.Controls.Add(Me.CmdEliminar)
        Me.Sup.Controls.Add(Me.txtregistros)
        Me.Sup.Controls.Add(Me.Label4)
        Me.Sup.Controls.Add(Me.lstClientes)
        Me.Sup.Controls.Add(Me.Label1)
        Me.Sup.Controls.Add(Me.CmdActualizar)
        Me.Sup.Controls.Add(Me.CmdGuardar)
        Me.Sup.Controls.Add(Me.CmdNuevo)
        Me.Sup.Controls.Add(Me.CmdCerrar)
        Me.Sup.Dock = System.Windows.Forms.DockStyle.Top
        Me.Sup.Location = New System.Drawing.Point(0, 0)
        Me.Sup.Name = "Sup"
        Me.Sup.Size = New System.Drawing.Size(964, 87)
        Me.Sup.TabIndex = 0
        '
        'Infe
        '
        Me.Infe.Controls.Add(Me.Tabla)
        Me.Infe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Infe.Location = New System.Drawing.Point(0, 87)
        Me.Infe.Name = "Infe"
        Me.Infe.Size = New System.Drawing.Size(964, 495)
        Me.Infe.TabIndex = 1
        '
        'CmdActualizar
        '
        Me.CmdActualizar.Image = Global.ATMFiscal.My.Resources.Resources.Actualizar
        Me.CmdActualizar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdActualizar.Location = New System.Drawing.Point(229, 12)
        Me.CmdActualizar.Name = "CmdActualizar"
        Me.CmdActualizar.Size = New System.Drawing.Size(50, 54)
        Me.CmdActualizar.TabIndex = 8
        Me.CmdActualizar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdActualizar.ThemeName = "Aqua"
        '
        'CmdEliminar
        '
        Me.CmdEliminar.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminar.Location = New System.Drawing.Point(173, 12)
        Me.CmdEliminar.Name = "CmdEliminar"
        Me.CmdEliminar.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminar.TabIndex = 6
        Me.CmdEliminar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminar.ThemeName = "Aqua"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(117, 12)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardar.TabIndex = 7
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'CmdNuevo
        '
        Me.CmdNuevo.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevo.Location = New System.Drawing.Point(61, 12)
        Me.CmdNuevo.Name = "CmdNuevo"
        Me.CmdNuevo.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevo.TabIndex = 5
        Me.CmdNuevo.TabStop = False
        Me.CmdNuevo.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevo.ThemeName = "Aqua"
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = CType(resources.GetObject("CmdCerrar.Image"), System.Drawing.Image)
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(5, 12)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.CmdCerrar.TabIndex = 4
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'lstClientes
        '
        Me.lstClientes.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstClientes.Location = New System.Drawing.Point(289, 31)
        Me.lstClientes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstClientes.Name = "lstClientes"
        Me.lstClientes.SelectItem = ""
        Me.lstClientes.SelectText = ""
        Me.lstClientes.Size = New System.Drawing.Size(421, 36)
        Me.lstClientes.TabIndex = 648
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(285, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 21)
        Me.Label1.TabIndex = 647
        Me.Label1.Text = "Empresa:"
        '
        'txtregistros
        '
        Me.txtregistros.AutoSize = True
        Me.txtregistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.txtregistros.Location = New System.Drawing.Point(875, 36)
        Me.txtregistros.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtregistros.Name = "txtregistros"
        Me.txtregistros.Size = New System.Drawing.Size(21, 21)
        Me.txtregistros.TabIndex = 650
        Me.txtregistros.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(717, 36)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(158, 21)
        Me.Label4.TabIndex = 649
        Me.Label4.Text = "Total de registros:"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Tabla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.Tabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.EnableHeadersVisualStyles = False
        Me.Tabla.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Tabla.GridColor = System.Drawing.Color.SteelBlue
        Me.Tabla.Location = New System.Drawing.Point(0, 0)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Tomato
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Tomato
        Me.Tabla.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.Tabla.Size = New System.Drawing.Size(964, 495)
        Me.Tabla.TabIndex = 5
        '
        'ListaNegra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(964, 582)
        Me.Controls.Add(Me.Infe)
        Me.Controls.Add(Me.Sup)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ListaNegra"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lista Negra de Contribuyentes"
        Me.ThemeName = "Material"
        CType(Me.Sup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Sup.ResumeLayout(False)
        Me.Sup.PerformLayout()
        CType(Me.Infe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Infe.ResumeLayout(False)
        CType(Me.CmdActualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Sup As Telerik.WinControls.UI.RadPanel
    Friend WithEvents Infe As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdActualizar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdNuevo As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents lstClientes As Listas
    Friend WithEvents Label1 As Label
    Friend WithEvents txtregistros As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Tabla As Tablas
End Class

