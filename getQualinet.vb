Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports MySql.Data.MySqlClient
Imports System.Threading

''' <summary>
''' busca dados do qualinet
''' </summary>
''' <remarks></remarks>
Public Class getQualinet
    Inherits MySql_Repository

    Private Property sqlQuery As New Query
    Public Property LogWriter As New LogWriter


    Sub AT5()

        Dim RESULT As String = Nothing
        Dim _SQL As String

        Dim codOperadora As String = "75,78,83,86,87,88,89,91,93,193,278,294,330,332,333,363,389,430,437,531,584,603,641,684,685,686,687,688,689,690,691,692,693,694,695,700,710,752,757,758,770,794,884,923,996"

        Dim SSQL As String = sqlQuery.at5(Format(CDate(Now).AddDays(-1), "yyyy-MM-dd"), codOperadora)

        Try
            Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
                qConn.Open()
                Dim qComm As New MySqlCommand(sqlQuery.at5(Format(CDate(Now).AddDays(-1), "yyyy-MM-dd"), codOperadora), qConn)
                Using ReadRows As MySqlDataReader = qComm.ExecuteReader()
                    While ReadRows.Read

                        _SQL = "('" & ReadRows.Item("SIGLA") & "', " & _
                            "'" & ReadRows.Item("ID_REGIAO") & "', " & _
                            "'" & ReadRows.Item("STATUS") & "', " & _
                            "'" & Format(CDate(ReadRows.Item("DT_NOTA")), "yyyy-MM-dd") & "', " & _
                            ReadRows.Item("COD_OS") & ", " & _
                            ReadRows.Item("NUM_CONTRATO") & ", " & _
                            "'" & ReadRows.Item("NOME_TITULAR") & "', " & _
                            "'" & ReadRows.Item("FECHAMENTO") & "', " & _
                            "'" & ReadRows.Item("SEGMENTO") & "', " & _
                            "'" & ReadRows.Item("TIPO_ORD_SRV") & "', " & _
                            "'" & ReadRows.Item("DT_INST_ASS").ToString & "', " & _
                            "'" & ReadRows.Item("DT_ATEND").ToString & "', " & _
                            "'" & ReadRows.Item("DT_CADASTRO").ToString & "', " & _
                            "'" & ReadRows.Item("DT_BAIXA").ToString & "', " & _
                            ReadRows.Item("COD_BAIXA") & ", " & _
                            "'" & ReadRows.Item("COD_CANCEL") & "', " & _
                            "'" & ReadRows.Item("ULT_REAGENDAMENTO_GERAL") & "', " & _
                            "'" & ReadRows.Item("ULT_USR_REAGENDAMENTO_GERAL") & "', " & _
                            "'" & ReadRows.Item("COD_BAIXA_1") & "', " & _
                            "'" & ReadRows.Item("COD_BAIXA_2") & "', " & _
                            "'" & ReadRows.Item("COD_BAIXA_3") & "', " & _
                            "'" & ReadRows.Item("COD_BAIXA_4") & "', " & _
                            "'" & ReadRows.Item("COD_BAIXA_5") & "', " & _
                            "'" & ReadRows.Item("LOG_REAGENDA") & "', " & _
                             IIf(IsDBNull(ReadRows.Item("LOG_VT")), "0", ReadRows.Item("LOG_VT")) & ", " & _
                            "'" & ReadRows.Item("COD_OS_1") & "', " & _
                            "'" & ReadRows.Item("COD_OS_2") & "', " & _
                            "'" & ReadRows.Item("COD_OS_3") & "', " & _
                            "'" & ReadRows.Item("COD_OS_4") & "', " & _
                            "'" & ReadRows.Item("COD_OS_5") & "', " & _
                            "'" & ReadRows.Item("ULT_COD_BAIXA") & "', " & _
                            "'" & ReadRows.Item("ULT_COD_OS") & "', " & _
                            "'" & ReadRows.Item("AREA_DESPACHO") & "', " & _
                            "'" & ReadRows.Item("COD_NODE") & "', " & _
                            ReadRows.Item("COD_IMOVEL") & ", " & _
                            "'" & ReadRows.Item("END_COMPLETO") & "', " & _
                            "'" & ReadRows.Item("TIPO_COMPLEMENTO") & "', " & _
                            "'" & ReadRows.Item("NR_PROTOCOLO_BP") & "', " & _
                            "'" & ReadRows.Item("DDD_TELEFONE_VOIP") & "', " & _
                            "'" & ReadRows.Item("NUM_TELEFONE_VOIP") & "', " & _
                            "'" & ReadRows.Item("DT_DESPACHO").ToString & "', " & _
                            "'" & ReadRows.Item("DESP_PARCEIRA") & "', " & _
                            "'" & ReadRows.Item("DESP_EQUIPE") & "', " & _
                            "'" & ReadRows.Item("EXEC_PARCEIRA") & "', " & _
                            "'" & ReadRows.Item("EXEC_EQUIPE") & "', " & _
                            "'" & ReadRows.Item("HR_INICIO_EXECUCAO").ToString & "', " & _
                            "'" & ReadRows.Item("HR_TERMINO_EXECUCAO").ToString & "', " & _
                            "'" & ReadRows.Item("DT_AGENDA").ToString & "', " & _
                            "'" & ReadRows.Item("AGENDA_DESCR") & "', " & _
                            "'" & ReadRows.Item("CONVENIENCIA_AUTO") & "', " & _
                            "'" & ReadRows.Item("EMERGENCIA") & "', " & _
                            "'" & ReadRows.Item("IMEDIATA") & "', " & _
                            "'" & ReadRows.Item("ISENTO_COBRANCA") & "', " & _
                            "'" & ReadRows.Item("NO_OCORRENCIA_IE") & "', " & _
                            ReadRows.Item("ID_PONTO") & ", " & _
                            "'" & ReadRows.Item("PRODUTO_TIPO_ANTIGO") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_TECNOLOG_ANTIGO") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_ANTIGO") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_TIPO_ATUAL") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_TECNOLOG_ATUAL") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_ATUAL") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_TIPO_NOVO") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_TECNOLOG_NOVO") & "', " & _
                            "'" & ReadRows.Item("PRODUTO_NOVO") & "', " & _
                            "'" & ReadRows.Item("USR_ATEND") & "', " & _
                            "'" & ReadRows.Item("USR_DESPACHO") & "', " & _
                            "'" & ReadRows.Item("USR_BAIXA") & "', " & _
                            "'" & ReadRows.Item("USR_ATEND_PF") & "', " & _
                            "'" & ReadRows.Item("USR_DESPACHO_PF") & "', " & _
                            "'" & ReadRows.Item("USR_BAIXA_PF") & "', " & _
                            "'" & ReadRows.Item("IE_DESCRICAO") & "', " & _
                            "'" & ReadRows.Item("IE_DT_OCORRENCIA").ToString & "', " & _
                            "'" & ReadRows.Item("IE_DT_RESOLUCAO").ToString & "', " & _
                            "'" & ReadRows.Item("IE_TP_RESOLUCAO") & "', " & _
                            "'" & ReadRows.Item("IE_USR_ATEND") & "', " & _
                            "'" & ReadRows.Item("IE_USR_RESOL") & "', " & _
                            "'" & ReadRows.Item("IE_OBS") & "', " & _
                            "'" & ReadRows.Item("IE_OBS_RESOL") & "') "

                        _SQL = Replace(_SQL, ", ,", ",'',")

                        MysqlAdd("IGNORE", "ora_qnet.qnet_at5", _SQL, "")
                    End While
                End Using
            End Using
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message, "AT5 UPDATE")
        End Try



    End Sub

End Class
