Imports System.Data.Odbc

Public Class Form1

    Private stringDeConexion As String = "DRIVER=MySQL ODBC 8.0 UNICODE Driver;UID=root;PWD=123;PORT=3307;DATABASE=prueba;SERVER=localhost"

    Private lector As OdbcDataReader

    Private Sub obtenerDatos()
        Dim conexion As New OdbcConnection(stringDeConexion)
        conexion.Open()

        Dim comando As New OdbcCommand
        comando.CommandText = "SELECT * FROM persona"

        comando.Connection = conexion

        Me.lector = comando.ExecuteReader()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim conexion As New OdbcConnection(stringDeConexion)
        conexion.Open()

        Dim comando As New OdbcCommand
        If txtMail.Text = "" Then
            comando.CommandText = "INSERT INTO persona(id,nombre,apellido) VALUES(" + txtId.Text + ",'" + txtNombre.Text + "','" + txtApellido.Text + "')"
        Else
            comando.CommandText = "INSERT INTO persona(id,nombre,apellido,mail) VALUES(" + txtId.Text + ",'" + txtNombre.Text + "','" + txtApellido.Text + "','" + txtMail.Text + "')"

        End If
        MsgBox(comando.CommandText)
        comando.Connection = conexion

        comando.ExecuteNonQuery()

        MsgBox("Persona insertada correctamente")

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim conexion As New OdbcConnection(stringDeConexion)
        conexion.Open()

        Dim comando As New OdbcCommand

        comando.CommandText = "UPDATE persona SET nombre = '" + txtNombre.Text + "', apellido = '" + txtApellido.Text + "', mail = '" + txtMail.Text + "' WHERE id = " + txtId.Text + ""

        MsgBox(comando.CommandText)
        comando.Connection = conexion

        comando.ExecuteNonQuery()

        MsgBox("Persona modificada correctamente")
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim conexion As New OdbcConnection(stringDeConexion)
        conexion.Open()

        Dim comando As New OdbcCommand

        comando.CommandText = "DELETE FROM persona WHERE id = " + txtId.Text + ""

        MsgBox(comando.CommandText)
        comando.Connection = conexion

        comando.ExecuteNonQuery()

        MsgBox("Persona eliminada correctamente")
    End Sub


    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        lector.Read()
        txtId.Text = lector(0).ToString()
        txtNombre.Text = lector(1).ToString()
        txtApellido.Text = lector(2).ToString()
        txtMail.Text = lector(3).ToString()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        obtenerDatos()

    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        ' Llenamos el DataReader con el resultado de la query
        obtenerDatos()

        ' Definimos un DataTable (tabla en memoria)
        Dim tabla As New DataTable

        ' Llenamos el DataTable con el contenido del DataReader (resultado de la query)
        tabla.Load(Me.lector)

        ' Asginamos el contenido del DataTable al DataGridView para visualizarlo
        GrillaDatos.DataSource = tabla
    End Sub

    Private Sub btnContar_Click(sender As Object, e As EventArgs) Handles btnContar.Click

        Dim conexion As New OdbcConnection(stringDeConexion)
            conexion.Open()

            Dim comando As New OdbcCommand

            comando.CommandText = "SELECT COUNT(*) FROM persona"
            comando.Connection = conexion

            MsgBox("Cantidad: " + comando.ExecuteScalar().ToString())


    End Sub
End Class
