<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Letras_Contabilizacion
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Letras_Contabilizacion))
		Me.Tabla_detalleLetras = New ATMFiscal.tabla_detalle()
		Me.lstCliente = New ATMFiscal.Listas()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Tabla_detalleLetras
		'
		Me.Tabla_detalleLetras.Cmdcerrar_enabled = True
		Me.Tabla_detalleLetras.CmdEditar_Enabled = True
		Me.Tabla_detalleLetras.CmdEliminar_enabled = True
		Me.Tabla_detalleLetras.CmdNuevo_Enabled = True
		Me.Tabla_detalleLetras.CmdRefrescar_enabled = True
		Me.Tabla_detalleLetras.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Tabla_detalleLetras.Location = New System.Drawing.Point(0, 0)
		Me.Tabla_detalleLetras.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
		Me.Tabla_detalleLetras.Name = "Tabla_detalleLetras"
		Me.Tabla_detalleLetras.Size = New System.Drawing.Size(894, 272)
		Me.Tabla_detalleLetras.SqlSelect = ""
		Me.Tabla_detalleLetras.TabIndex = 0
		Me.Tabla_detalleLetras.Tag = "P_Master"
		'
		'lstCliente
		'
		Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lstCliente.Location = New System.Drawing.Point(607, 33)
		Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.lstCliente.Name = "lstCliente"
		Me.lstCliente.SelectItem = ""
		Me.lstCliente.SelectText = ""
		Me.lstCliente.Size = New System.Drawing.Size(205, 36)
		Me.lstCliente.TabIndex = 95
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.Color.LightSteelBlue
		Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(603, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(76, 17)
		Me.Label1.TabIndex = 94
		Me.Label1.Text = "Empresa:"
		'
		'Letras_Contabilizacion
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(894, 272)
		Me.Controls.Add(Me.lstCliente)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Tabla_detalleLetras)
		Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "Letras_Contabilizacion"
		'
		'
		'
		Me.RootElement.ApplyShapeToControl = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Contabilización"
		Me.ThemeName = "Material"
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Tabla_detalleLetras As tabla_detalle
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
End Class

