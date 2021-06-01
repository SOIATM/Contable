<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Obligaciones
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Obligaciones))
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.LstEmpresa = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdEliminarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevoF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cargo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Abono = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SF = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.LstEmpresa)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.CmdEliminarF)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdNuevoF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(880, 86)
        Me.RadPanel1.TabIndex = 2
        '
        'LstEmpresa
        '
        Me.LstEmpresa.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstEmpresa.Location = New System.Drawing.Point(312, 33)
        Me.LstEmpresa.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstEmpresa.Name = "LstEmpresa"
        Me.LstEmpresa.SelectItem = ""
        Me.LstEmpresa.SelectText = ""
        Me.LstEmpresa.Size = New System.Drawing.Size(421, 36)
        Me.LstEmpresa.TabIndex = 637
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(308, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 636
        Me.Label3.Text = "Empresa:"
        '
        'CmdEliminarF
        '
        Me.CmdEliminarF.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminarF.Location = New System.Drawing.Point(164, 2)
        Me.CmdEliminarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdEliminarF.Name = "CmdEliminarF"
        Me.CmdEliminarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdEliminarF.TabIndex = 635
        Me.CmdEliminarF.TabStop = False
        Me.CmdEliminarF.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdEliminarF.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(2, 2)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 632
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(218, 2)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 634
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdNuevoF
        '
        Me.CmdNuevoF.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevoF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevoF.Location = New System.Drawing.Point(110, 2)
        Me.CmdNuevoF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdNuevoF.Name = "CmdNuevoF"
        Me.CmdNuevoF.Size = New System.Drawing.Size(50, 54)
        Me.CmdNuevoF.TabIndex = 631
        Me.CmdNuevoF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdNuevoF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(56, 2)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 633
        Me.CmdBuscarFact.TabStop = False
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Descripcion, Me.Cargo, Me.Abono, Me.SF})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 86)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(880, 542)
        Me.TablaImportar.TabIndex = 3
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Visible = False
        '
        'Descripcion
        '
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        '
        'Cargo
        '
        Me.Cargo.HeaderText = "Cargo"
        Me.Cargo.Name = "Cargo"
        Me.Cargo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Cargo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Abono
        '
        Me.Abono.HeaderText = "Abono"
        Me.Abono.Name = "Abono"
        Me.Abono.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Abono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'SF
        '
        Me.SF.HeaderText = "SF"
        Me.SF.Name = "SF"
        Me.SF.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Obligaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(880, 628)
        Me.ControlBox = False
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Obligaciones"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Obligaciones"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdEliminarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevoF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents LstEmpresa As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents CmdEliminarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdNuevoF As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents ID As DataGridViewTextBoxColumn
	Friend WithEvents Descripcion As DataGridViewTextBoxColumn
	Friend WithEvents Cargo As DataGridViewCheckBoxColumn
	Friend WithEvents Abono As DataGridViewCheckBoxColumn
	Friend WithEvents SF As DataGridViewCheckBoxColumn
End Class

