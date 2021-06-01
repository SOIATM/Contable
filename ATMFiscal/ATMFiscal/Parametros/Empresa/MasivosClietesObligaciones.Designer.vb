<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasivosClietesObligaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasivosClietesObligaciones))
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.comboAño = New Telerik.WinControls.UI.RadDropDownList()
        Me.CmdGuardarF = New Telerik.WinControls.UI.RadButton()
        Me.CmdSalirF = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarFact = New Telerik.WinControls.UI.RadButton()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.IdO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Obligacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Enero = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Febrero = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Marzo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Abril = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Mayo = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Junio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Julio = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Agosto = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Septiembre = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Octubre = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Noviembre = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Diciembre = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SP1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label6)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.comboAño)
        Me.RadPanel1.Controls.Add(Me.CmdGuardarF)
        Me.RadPanel1.Controls.Add(Me.CmdSalirF)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarFact)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1092, 83)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(189, 31)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(405, 41)
        Me.lstCliente.TabIndex = 607
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(186, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 18)
        Me.Label6.TabIndex = 606
        Me.Label6.Text = "Empresa:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(664, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 18)
        Me.Label2.TabIndex = 605
        Me.Label2.Text = "Año:"
        '
        'comboAño
        '
        Me.comboAño.Location = New System.Drawing.Point(630, 31)
        Me.comboAño.Name = "comboAño"
        Me.comboAño.Size = New System.Drawing.Size(125, 36)
        Me.comboAño.TabIndex = 604
        Me.comboAño.Text = " "
        Me.comboAño.ThemeName = "Material"
        '
        'CmdGuardarF
        '
        Me.CmdGuardarF.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardarF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardarF.Location = New System.Drawing.Point(119, 13)
        Me.CmdGuardarF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdGuardarF.Name = "CmdGuardarF"
        Me.CmdGuardarF.Size = New System.Drawing.Size(50, 54)
        Me.CmdGuardarF.TabIndex = 511
        Me.CmdGuardarF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardarF.ThemeName = "Aqua"
        '
        'CmdSalirF
        '
        Me.CmdSalirF.Image = CType(resources.GetObject("CmdSalirF.Image"), System.Drawing.Image)
        Me.CmdSalirF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdSalirF.Location = New System.Drawing.Point(11, 11)
        Me.CmdSalirF.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdSalirF.Name = "CmdSalirF"
        Me.CmdSalirF.Size = New System.Drawing.Size(50, 54)
        Me.CmdSalirF.TabIndex = 509
        Me.CmdSalirF.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdSalirF.ThemeName = "Aqua"
        '
        'CmdBuscarFact
        '
        Me.CmdBuscarFact.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscarFact.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarFact.Location = New System.Drawing.Point(65, 11)
        Me.CmdBuscarFact.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarFact.Name = "CmdBuscarFact"
        Me.CmdBuscarFact.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarFact.TabIndex = 510
        Me.CmdBuscarFact.TabStop = False
        Me.CmdBuscarFact.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarFact.ThemeName = "Aqua"
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IdO, Me.Obligacion, Me.Enero, Me.Febrero, Me.Marzo, Me.Abril, Me.Mayo, Me.Junio, Me.Julio, Me.Agosto, Me.Septiembre, Me.Octubre, Me.Noviembre, Me.Diciembre})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 83)
        Me.Tabla.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1092, 452)
        Me.Tabla.TabIndex = 1
        '
        'IdO
        '
        Me.IdO.HeaderText = "Id_Obligacion"
        Me.IdO.Name = "IdO"
        Me.IdO.Visible = False
        '
        'Obligacion
        '
        Me.Obligacion.HeaderText = "Obligacion"
        Me.Obligacion.Name = "Obligacion"
        '
        'Enero
        '
        Me.Enero.HeaderText = "Enero"
        Me.Enero.Name = "Enero"
        Me.Enero.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Enero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Febrero
        '
        Me.Febrero.HeaderText = "Febrero"
        Me.Febrero.Name = "Febrero"
        Me.Febrero.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Febrero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Marzo
        '
        Me.Marzo.HeaderText = "Marzo"
        Me.Marzo.Name = "Marzo"
        Me.Marzo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Marzo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Abril
        '
        Me.Abril.HeaderText = "Abril"
        Me.Abril.Name = "Abril"
        Me.Abril.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Abril.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Mayo
        '
        Me.Mayo.HeaderText = "Mayo"
        Me.Mayo.Name = "Mayo"
        Me.Mayo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Mayo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Junio
        '
        Me.Junio.HeaderText = "Junio"
        Me.Junio.Name = "Junio"
        Me.Junio.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Junio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Julio
        '
        Me.Julio.HeaderText = "Julio"
        Me.Julio.Name = "Julio"
        Me.Julio.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Julio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Agosto
        '
        Me.Agosto.HeaderText = "Agosto"
        Me.Agosto.Name = "Agosto"
        Me.Agosto.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Agosto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Septiembre
        '
        Me.Septiembre.HeaderText = "Septiembre"
        Me.Septiembre.Name = "Septiembre"
        Me.Septiembre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Septiembre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Octubre
        '
        Me.Octubre.HeaderText = "Octubre"
        Me.Octubre.Name = "Octubre"
        Me.Octubre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Octubre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Noviembre
        '
        Me.Noviembre.HeaderText = "Noviembre"
        Me.Noviembre.Name = "Noviembre"
        Me.Noviembre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Noviembre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Diciembre
        '
        Me.Diciembre.HeaderText = "Diciembre"
        Me.Diciembre.Name = "Diciembre"
        Me.Diciembre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Diciembre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'SP1
        '
        Me.SP1.WorkerSupportsCancellation = True
        '
        'MasivosClietesObligaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1092, 535)
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MasivosClietesObligaciones"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Masivos Clientes Obligaciones"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.comboAño, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardarF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdSalirF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarFact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdGuardarF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdSalirF As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdBuscarFact As Telerik.WinControls.UI.RadButton
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents Tabla As DataGridView
    Friend WithEvents IdO As DataGridViewTextBoxColumn
    Friend WithEvents Obligacion As DataGridViewTextBoxColumn
    Friend WithEvents Enero As DataGridViewCheckBoxColumn
    Friend WithEvents Febrero As DataGridViewCheckBoxColumn
    Friend WithEvents Marzo As DataGridViewCheckBoxColumn
    Friend WithEvents Abril As DataGridViewCheckBoxColumn
    Friend WithEvents Mayo As DataGridViewCheckBoxColumn
    Friend WithEvents Junio As DataGridViewCheckBoxColumn
    Friend WithEvents Julio As DataGridViewCheckBoxColumn
    Friend WithEvents Agosto As DataGridViewCheckBoxColumn
    Friend WithEvents Septiembre As DataGridViewCheckBoxColumn
    Friend WithEvents Octubre As DataGridViewCheckBoxColumn
    Friend WithEvents Noviembre As DataGridViewCheckBoxColumn
    Friend WithEvents Diciembre As DataGridViewCheckBoxColumn
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label6 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents comboAño As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents SP1 As System.ComponentModel.BackgroundWorker
End Class

