Imports System.Data.SqlClient

Public Class daoVisitantes
    Dim strConn As String = My.Settings.StrConexion
    Public Function AgregarRegistro(ByVal visitante As VisitantesEntidad) As Boolean
        Dim resp As Boolean = False
        Try
            Dim tsql As String = "Insert into Visitantes(Nombres, Apellidos, Email, Telefono, Fechavisita, Ciudad) values(@nombres, @apellidos, @email, @telefono, @fechavisita, @ciudad)"
            Dim conn As New SqlConnection(strConn)
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = tsql
            cmd.Parameters.AddWithValue("@nombres", visitante.Nombres)
            cmd.Parameters.AddWithValue("@apellidos", visitante.Apellidos)
            cmd.Parameters.AddWithValue("@email", visitante.Email)
            cmd.Parameters.AddWithValue("@telefono", visitante.Telefono)
            cmd.Parameters.AddWithValue("@fechavisita", visitante.Fechavisita)
            cmd.Parameters.AddWithValue("@ciudad", visitante.Ciudad)
            cmd.Connection = conn
            cmd.Connection.Open()
            If cmd.ExecuteNonQuery Then
                resp = True
            End If
            cmd.Connection.Close()
        Catch ex As Exception
            resp = False
        End Try
        Return resp
    End Function

    Public Function EditarRegistro(ByVal visitante As VisitantesEntidad) As Boolean
        Dim resp As Boolean = False
        Try
            Dim tsql As String = "update Visitantes set Nombres = @nombres, Apellidos = @apellidos, Email = @email, Telefono = @telefono, Fechavisita = @fechavisita, Ciudad = @ciudad where Id = @id"
            Dim conn As New SqlConnection(strConn)
            conn.Open()
            Dim cmd As New SqlCommand(tsql, conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@nombres", visitante.Nombres)
            cmd.Parameters.AddWithValue("@apellidos", visitante.Apellidos)
            cmd.Parameters.AddWithValue("@email", visitante.Email)
            cmd.Parameters.AddWithValue("@telefono", visitante.Telefono)
            cmd.Parameters.AddWithValue("@fechavisita", visitante.Fechavisita)
            cmd.Parameters.AddWithValue("@ciudad", visitante.Ciudad)
            cmd.Parameters.AddWithValue("@id", visitante.Id)
            If (cmd.ExecuteNonQuery <> 0) Then
                resp = True
            End If
            conn.Close()
        Catch ex As Exception
            resp = False
        End Try
        Return resp
    End Function

    Public Function EliminarRegistro(ByVal id As Integer) As Boolean
        Dim resp As Boolean = False
        Try
            Dim tsql As String = "delete from Visitantes where Id = @id"
            Dim conn As New SqlConnection(strConn)
            conn.Open()
            Dim cmd As New SqlCommand(tsql, conn)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@id", id)
            If (cmd.ExecuteNonQuery <> 0) Then
                resp = True
            End If
            conn.Close()
        Catch ex As Exception
            resp = False
        End Try
        Return resp
    End Function

    Public Function MostrarRegistros() As DataTable
        Dim dt As New DataTable
        Try
            Dim tsql As String = "select * from Visitantes"
            Dim conn As New SqlConnection(strConn)
            Dim da As New SqlDataAdapter(tsql, conn)
            da.Fill(dt)
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Function MostrarRegistrosPorCiudad(ByVal ciudad As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim tsql As String = "select * from Visitantes where Ciudad = @ciudad"
            Dim conn As New SqlConnection(strConn)
            Dim da As New SqlDataAdapter(tsql, conn)
            da.SelectCommand.Parameters.AddWithValue("@ciudad", ciudad)
            da.Fill(dt)
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Function MostrarRegistrosPorRangoDeFechas(ByVal fechaInicial As Date, ByVal fechaFinal As Date) As DataTable
        Dim dt As New DataTable
        Try
            Dim tsql As String = "select * from Visitantes where Fechavisita between @fechaInicial and @fechaFinal"
            Dim conn As New SqlConnection(strConn)
            Dim da As New SqlDataAdapter(tsql, conn)
            da.SelectCommand.Parameters.AddWithValue("@fechaInicial", fechaInicial)
            da.SelectCommand.Parameters.AddWithValue("@fechaFinal", fechaFinal)
            da.Fill(dt)
        Catch ex As Exception

        End Try
        Return dt
    End Function

    Public Function MostrarRegistrosPorNombres(ByVal nombres As String) As DataTable
        Dim dt As New DataTable
        Try
            Dim tsql As String = "select * from Visitantes where Nombres like @nombres"
            Dim conn As New SqlConnection(strConn)
            Dim da As New SqlDataAdapter(tsql, conn)
            da.SelectCommand.Parameters.AddWithValue("@nombres", "%" & nombres & "%")
            da.Fill(dt)
        Catch ex As Exception
        End Try
        Return dt
    End Function

    Public Function BuscarRegistro(ByVal id As Integer) As VisitantesEntidad
        Dim visitante As New VisitantesEntidad
        Try
            Dim tsql As String = "select * from Visitantes where Id = @id"
            Dim conn As New SqlConnection(strConn)
            Dim tbl As New DataTable
            Dim da As New SqlDataAdapter(tsql, conn)
            da.SelectCommand.Parameters.AddWithValue("@id", id)
            da.Fill(tbl)
            If tbl.Rows.Count > 0 Then
                visitante.Id = tbl.Rows(0).Item("Id")
                visitante.Nombres = tbl.Rows(0).Item("Nombres")
                visitante.Apellidos = tbl.Rows(0).Item("Apellidos")
                visitante.Email = tbl.Rows(0).Item("Email")
                visitante.Telefono = tbl.Rows(0).Item("Telefono")
                visitante.Fechavisita = tbl.Rows(0).Item("Fechavisita")
                visitante.Ciudad = tbl.Rows(0).Item("Ciudad")
            End If
        Catch ex As Exception

        End Try
        Return visitante
    End Function

End Class
