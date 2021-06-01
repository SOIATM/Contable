<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tipo_Poliza_SAT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tipo_Poliza_SAT))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_detallePolizasSat = New ATMFiscal.tabla_detalle()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_detallePolizasSat
        '
        Me.Tabla_detallePolizasSat.Cmdcerrar_enabled = True
        Me.Tabla_detallePolizasSat.CmdEditar_Enabled = True
        Me.Tabla_detallePolizasSat.CmdEliminar_enabled = True
        Me.Tabla_detallePolizasSat.CmdNuevo_Enabled = True
        Me.Tabla_detallePolizasSat.CmdRefrescar_enabled = True
        Me.Tabla_detallePolizasSat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detallePolizasSat.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detallePolizasSat.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_detallePolizasSat.Name = "Tabla_detallePolizasSat"
        Me.Tabla_detallePolizasSat.Size = New System.Drawing.Size(932, 328)
        Me.Tabla_detallePolizasSat.SqlSelect = ""
        Me.Tabla_detallePolizasSat.TabIndex = 0
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(468, 34)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(436, 36)
        Me.lstCliente.TabIndex = 95
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(464, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 21)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "Empresa:"
        '
        'Tipo_Poliza_SAT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 328)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstCliente)
        Me.Controls.Add(Me.Tabla_detallePolizasSat)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Tipo_Poliza_SAT"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tipo Pólizas SAT"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Tabla_detallePolizasSat As tabla_detalle
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label1 As Label
End Class

