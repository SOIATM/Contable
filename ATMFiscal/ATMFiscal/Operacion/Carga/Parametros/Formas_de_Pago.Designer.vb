<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Formas_de_Pago
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Formas_de_Pago))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_detalleFormasPago = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_detalleFormasPago
        '
        Me.Tabla_detalleFormasPago.Cmdcerrar_enabled = True
        Me.Tabla_detalleFormasPago.CmdEditar_Enabled = True
        Me.Tabla_detalleFormasPago.CmdEliminar_enabled = True
        Me.Tabla_detalleFormasPago.CmdNuevo_Enabled = True
        Me.Tabla_detalleFormasPago.CmdRefrescar_enabled = True
        Me.Tabla_detalleFormasPago.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detalleFormasPago.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detalleFormasPago.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_detalleFormasPago.Name = "Tabla_detalleFormasPago"
        Me.Tabla_detalleFormasPago.Size = New System.Drawing.Size(715, 397)
        Me.Tabla_detalleFormasPago.SqlSelect = ""
        Me.Tabla_detalleFormasPago.TabIndex = 0
        Me.Tabla_detalleFormasPago.Tag = "P_Master"
        '
        'Formas_de_Pago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 397)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla_detalleFormasPago)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Formas_de_Pago"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Formas de Pago"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Tabla_detalleFormasPago As tabla_detalle
End Class

