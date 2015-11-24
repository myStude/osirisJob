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
    Private Property codOperadora As String = "75,78,83,86,87,88,89,91,93,193,278,294,330,332,333,363,389,430,437,531,584,603,641,684,685,686,687,688,689,690,691,692,693,694,695,700,710,752,757,758,770,794,884,923,996"

    Public Property LogWriter As New LogWriter

    ''' <summary>
    ''' GET AT5
    ''' </summary>
    ''' <param name="dia">YYYY-MM-DD</param>
    ''' <remarks>ENJOY</remarks>
    Sub AT5(dia As String)

        Dim RESULT As String = Nothing
        Dim _SQL As String
        Dim linha As Integer = 0


        Try
            Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
                qConn.Open()
                Using qComm As New MySqlCommand(sqlQuery.at5(dia, codOperadora), qConn)
                    Using rRows As MySqlDataReader = qComm.ExecuteReader()

                        For Each ReadRows In rRows

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
                                "'" & ReadRows.Item("IE_OBS").ToString.Replace("'", " - ") & "', " & _
                                "'" & ReadRows.Item("IE_OBS_RESOL").ToString.Replace("'", " - ") & "') "

                            _SQL = Replace(_SQL, ", ,", ",'',")
                            _SQL = Replace(_SQL, "/", "#")

                            MysqlAdd("IGNORE", "ora_qnet.qnet_at5", _SQL, "")

                        Next
                        MysqlDEL("ora_qnet.qnet_at5", "DT_NOTA < CURDATE() - INTERVAL 90 DAY")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message & "(" & linha & ")", "AT5 UPDATE")
        End Try
    End Sub

    ''' <summary>
    ''' GET AT9
    ''' </summary>
    ''' <param name="dia">YYYY-MM-DD</param>
    ''' <remarks>ENJOY</remarks>
    Sub AT9(dia As String)

        Dim RESULT As String = Nothing
        Dim _SQL As String
        Dim linha As Integer = 0


        Try
            Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
                qConn.Open()
                Using qComm As New MySqlCommand(sqlQuery.at9(dia, codOperadora), qConn)
                    Using rRows As MySqlDataReader = qComm.ExecuteReader()

                        For Each ReadRows In rRows

                            _SQL = "('" & ReadRows.Item("SIGLA") & "'," & _
                                    "'" & ReadRows.Item("ID_REGIAO") & "'," & _
                                    "'" & ReadRows.Item("STATUS") & "'," & _
                                    "'" & Format(ReadRows.Item("DT_NOTA"), "yyyy-MM-dd") & "'," & _
                                    "'" & ReadRows.Item("COD_OS") & "'," & _
                                    "'" & ReadRows.Item("NUM_CONTRATO") & "'," & _
                                    "'" & ReadRows.Item("NOME_TITULAR") & "'," & _
                                    "'" & ReadRows.Item("FECHAMENTO") & "'," & _
                                    "'" & ReadRows.Item("SEGMENTO") & "'," & _
                                    "'" & ReadRows.Item("TIPO_ORD_SRV") & "'," & _
                                    "'" & ReadRows.Item("DT_INST_ASS") & "'," & _
                                    "'" & ReadRows.Item("DT_ATEND") & "'," & _
                                    "'" & ReadRows.Item("DT_CADASTRO") & "'," & _
                                    "'" & ReadRows.Item("DT_BAIXA") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA") & "'," & _
                                    "'" & ReadRows.Item("COD_CANCEL") & "'," & _
                                    "'" & ReadRows.Item("ULT_REAGENDAMENTO_GERAL") & "'," & _
                                    "'" & ReadRows.Item("ULT_USR_REAGENDAMENTO_GERAL") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA_1") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA_2") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA_3") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA_4") & "'," & _
                                    "'" & ReadRows.Item("COD_BAIXA_5") & "'," & _
                                    "'" & ReadRows.Item("LOG_VT") & "'," & _
                                    "'" & ReadRows.Item("COD_OS_1") & "'," & _
                                    "'" & ReadRows.Item("COD_OS_2") & "'," & _
                                    "'" & ReadRows.Item("COD_OS_3") & "'," & _
                                    "'" & ReadRows.Item("COD_OS_4") & "'," & _
                                    "'" & ReadRows.Item("COD_OS_5") & "'," & _
                                    "'" & ReadRows.Item("ULT_COD_BAIXA") & "'," & _
                                    "'" & ReadRows.Item("ULT_COD_OS") & "'," & _
                                    "'" & ReadRows.Item("AREA_DESPACHO") & "'," & _
                                    "'" & ReadRows.Item("COD_NODE") & "'," & _
                                    "'" & ReadRows.Item("COD_IMOVEL") & "'," & _
                                    "'" & ReadRows.Item("END_COMPLETO") & "'," & _
                                    "'" & ReadRows.Item("TIPO_COMPLEMENTO") & "'," & _
                                    "'" & ReadRows.Item("NR_PROTOCOLO_BP") & "'," & _
                                    "'" & ReadRows.Item("DDD_TELEFONE_VOIP") & "'," & _
                                    "'" & ReadRows.Item("NUM_TELEFONE_VOIP") & "'," & _
                                    "'" & ReadRows.Item("DT_DESPACHO") & "'," & _
                                    "'" & ReadRows.Item("DESP_PARCEIRA") & "'," & _
                                    "'" & ReadRows.Item("DESP_EQUIPE") & "'," & _
                                    "'" & ReadRows.Item("EXEC_PARCEIRA") & "'," & _
                                    "'" & ReadRows.Item("EXEC_EQUIPE") & "'," & _
                                    "'" & ReadRows.Item("HR_INICIO_EXECUCAO") & "'," & _
                                    "'" & ReadRows.Item("HR_TERMINO_EXECUCAO") & "'," & _
                                    "'" & ReadRows.Item("DT_AGENDA") & "'," & _
                                    "'" & ReadRows.Item("AGENDA_DESCR") & "'," & _
                                    "'" & ReadRows.Item("CONVENIENCIA_AUTO") & "'," & _
                                    "'" & ReadRows.Item("EMERGENCIA") & "'," & _
                                    "'" & ReadRows.Item("IMEDIATA") & "'," & _
                                    "'" & ReadRows.Item("ISENTO_COBRANCA") & "'," & _
                                    "'" & ReadRows.Item("NO_OCORRENCIA_IE") & "'," & _
                                    "'" & ReadRows.Item("ID_PONTO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TIPO_ANTIGO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TECNOLOG_ANTIGO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_ANTIGO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TIPO_ATUAL") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TECNOLOG_ATUAL") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_ATUAL") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TIPO_NOVO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_TECNOLOG_NOVO") & "'," & _
                                    "'" & ReadRows.Item("PRODUTO_NOVO") & "'," & _
                                    "'" & ReadRows.Item("USR_ATEND") & "'," & _
                                    "'" & ReadRows.Item("USR_DESPACHO") & "'," & _
                                    "'" & ReadRows.Item("USR_BAIXA") & "'," & _
                                    "'" & ReadRows.Item("USR_ATEND_PF") & "'," & _
                                    "'" & ReadRows.Item("USR_DESPACHO_PF") & "'," & _
                                    "'" & ReadRows.Item("USR_BAIXA_PF") & "'," & _
                                    "'" & ReadRows.Item("IE_DESCRICAO") & "'," & _
                                    "'" & ReadRows.Item("IE_DT_OCORRENCIA") & "'," & _
                                    "'" & ReadRows.Item("IE_DT_RESOLUCAO") & "'," & _
                                    "'" & ReadRows.Item("IE_TP_RESOLUCAO") & "'," & _
                                    "'" & ReadRows.Item("IE_USR_ATEND") & "'," & _
                                    "'" & ReadRows.Item("IE_USR_RESOL") & "'," & _
                                    "'" & ReadRows.Item("IE_OBS").ToString.Replace("'", " - ") & "'," & _
                                    "'" & ReadRows.Item("IE_OBS_RESOL") & "')"


                            _SQL = Replace(_SQL, ", ,", ",'',")
                            _SQL = Replace(_SQL, "/", "#")

                            MysqlAdd("IGNORE", "ora_qnet.qnet_at9", _SQL, "")
                            _SQL = Nothing
                        Next
                        MysqlDEL("ora_qnet.qnet_at9", "DT_NOTA < CURDATE() - INTERVAL 90 DAY")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message & "(" & linha & ")", "AT9 UPDATE")
        End Try
    End Sub

    ''' <summary>
    ''' GET AT1
    ''' </summary>
    ''' <param name="dia">YYYY-MM-DD</param>
    ''' <remarks>ENJOY</remarks>
    Sub AT1(dia As String)

        Dim RESULT As String = Nothing
        Dim _SQL As String = ""
        Dim linha As Integer = 0

        Try
            Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
                qConn.Open()
                Using qComm As New MySqlCommand(sqlQuery.at1(dia, codOperadora), qConn)
                    Using rRows As MySqlDataReader = qComm.ExecuteReader()

                        For Each ReadRows In rRows

                            _SQL = "('" & ReadRows.Item("SIGLA") & "'," & _
                                "'" & ReadRows.Item("ID_REGIAO") & "'," & _
                                "'" & ReadRows.Item("STATUS") & "'," & _
                                "'" & Format(ReadRows.Item("DT_NOTA"), "yyyy-MM-dd") & "'," & _
                                "'" & ReadRows.Item("COD_OS") & "'," & _
                                "'" & ReadRows.Item("NUM_CONTRATO") & "'," & _
                                "'" & ReadRows.Item("NOME_TITULAR") & "'," & _
                                "'" & ReadRows.Item("FECHAMENTO") & "'," & _
                                "'" & ReadRows.Item("SEGMENTO") & "'," & _
                                "'" & ReadRows.Item("TIPO_ORD_SRV") & "'," & _
                                "'" & ReadRows.Item("DT_INST_ASS") & "'," & _
                                "'" & ReadRows.Item("DT_ATEND") & "'," & _
                                "'" & ReadRows.Item("DT_CADASTRO") & "'," & _
                                "'" & ReadRows.Item("DT_BAIXA") & "'," & _
                                "'" & ReadRows.Item("COD_BAIXA") & "'," & _
                                "'" & ReadRows.Item("COD_CANCEL") & "'," & _
                                "'" & ReadRows.Item("ULT_REAGENDAMENTO_GERAL") & "'," & _
                                "'" & ReadRows.Item("ULT_USR_REAGENDAMENTO_GERAL") & "'," & _
                                "'" & ReadRows.Item("AREA_DESPACHO") & "'," & _
                                "'" & ReadRows.Item("COD_NODE") & "'," & _
                                "'" & ReadRows.Item("COD_IMOVEL") & "'," & _
                                "'" & ReadRows.Item("END_COMPLETO") & "'," & _
                                "'" & ReadRows.Item("TIPO_COMPLEMENTO") & "'," & _
                                "'" & ReadRows.Item("NR_PROTOCOLO_BP") & "'," & _
                                "'" & ReadRows.Item("DDD_TELEFONE_VOIP") & "'," & _
                                "'" & ReadRows.Item("NUM_TELEFONE_VOIP") & "'," & _
                                "'" & ReadRows.Item("DT_DESPACHO") & "'," & _
                                "'" & ReadRows.Item("DESP_PARCEIRA") & "'," & _
                                "'" & ReadRows.Item("DESP_EQUIPE") & "'," & _
                                "'" & ReadRows.Item("EXEC_PARCEIRA") & "'," & _
                                "'" & ReadRows.Item("EXEC_EQUIPE") & "'," & _
                                "'" & ReadRows.Item("HR_INICIO_EXECUCAO") & "'," & _
                                "'" & ReadRows.Item("HR_TERMINO_EXECUCAO") & "'," & _
                                "'" & ReadRows.Item("DT_AGENDA") & "'," & _
                                "'" & ReadRows.Item("AGENDA_DESCR") & "'," & _
                                "'" & ReadRows.Item("CONVENIENCIA_AUTO") & "'," & _
                                "'" & ReadRows.Item("EMERGENCIA") & "'," & _
                                "'" & ReadRows.Item("IMEDIATA") & "'," & _
                                "'" & ReadRows.Item("ISENTO_COBRANCA") & "'," & _
                                "'" & ReadRows.Item("NO_OCORRENCIA_IE") & "'," & _
                                "'" & ReadRows.Item("ID_PONTO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TIPO_ANTIGO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TECNOLOG_ANTIGO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_ANTIGO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TIPO_ATUAL") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TECNOLOG_ATUAL") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_ATUAL") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TIPO_NOVO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_TECNOLOG_NOVO") & "'," & _
                                "'" & ReadRows.Item("PRODUTO_NOVO") & "'," & _
                                "'" & ReadRows.Item("USR_ATEND") & "'," & _
                                "'" & ReadRows.Item("USR_DESPACHO") & "'," & _
                                "'" & ReadRows.Item("USR_BAIXA") & "'," & _
                                "'" & ReadRows.Item("USR_ATEND_PF") & "'," & _
                                "'" & ReadRows.Item("USR_DESPACHO_PF") & "'," & _
                                "'" & ReadRows.Item("USR_BAIXA_PF") & "'," & _
                                "'" & ReadRows.Item("IE_DESCRICAO") & "'," & _
                                "'" & ReadRows.Item("IE_DT_OCORRENCIA") & "'," & _
                                "'" & ReadRows.Item("IE_DT_RESOLUCAO") & "'," & _
                                "'" & ReadRows.Item("IE_TP_RESOLUCAO") & "'," & _
                                "'" & ReadRows.Item("IE_USR_ATEND") & "'," & _
                                "'" & ReadRows.Item("IE_USR_RESOL") & "'," & _
                                "'" & ReadRows.Item("IE_OBS").ToString.Replace("'", " - ") & "'," & _
                                "'" & ReadRows.Item("IE_OBS_RESOL").ToString.Replace("'", " - ") & "')"

                            _SQL = Replace(_SQL, ", ,", ",'',")
                            _SQL = Replace(_SQL, "/", "#")

                            MysqlAdd("IGNORE", "ora_qnet.qnet_at1", _SQL, "")
                            _SQL = Nothing
                        Next

                        '// DELETA MAIOR QUE 90 DIAS
                        MysqlDEL("ora_qnet.qnet_at1", "DT_NOTA < CURDATE() - INTERVAL 90 DAY")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message & "(" & linha & ")", "AT1 UPDATE")
        End Try
    End Sub


    ''' <summary>
    ''' SLA IE
    ''' </summary>
    ''' <param name="dia">yyyy-MM-dd</param>
    ''' <remarks></remarks>
    Sub SlaIE(dia As String)

        Dim RESULT As String = Nothing
        Dim _SQL As String = ""
        Dim linha As Integer = 0

        Try
            Using qConn As New MySqlConnection(qualinetConnection("paq_last"))
                qConn.Open()
                Using qComm As New MySqlCommand(sqlQuery.SLAiE(dia, codOperadora), qConn)
                    Using rRows As MySqlDataReader = qComm.ExecuteReader()

                        For Each ReadRows In rRows

                            _SQL = "('" & ReadRows.Item("SIGLA") & "'," & _
                                    "'" & ReadRows.Item("ID_REGIAO") & "'," & _
                                    "'" & ReadRows.Item("STATUS") & "'," & _
                                    "'" & Format(ReadRows.Item("DT_NOTA"), "yyyy-MM-dd") & "'," & _
                                    "'" & ReadRows.Item("ID_OCORRENCIA") & "'," & _
                                    "'" & ReadRows.Item("NUM_CONTRATO") & "'," & _
                                    "'" & ReadRows.Item("SEGMENTO_DESCR") & "'," & _
                                    "'" & ReadRows.Item("TIPO_OCORR") & "'," & _
                                    "'" & ReadRows.Item("OR_DESCRICAO") & "'," & _
                                    "'" & ReadRows.Item("DT_OCORRENCIA") & "'," & _
                                    "'" & ReadRows.Item("DT_RESOLUCAO") & "'," & _
                                    "'" & ReadRows.Item("DT_TRATAMENTO") & "'," & _
                                    "'" & ReadRows.Item("DT_MAX_ATEND") & "'," & _
                                    "'" & ReadRows.Item("AREA_DESPACHO") & "'," & _
                                    "'" & ReadRows.Item("COD_NODE") & "'," & _
                                    "'" & ReadRows.Item("COD_IMOVEL") & "'," & _
                                    "'" & ReadRows.Item("END_COMPLETO") & "'," & _
                                    "'" & ReadRows.Item("TIPO_COMPLEMENTO") & "'," & _
                                    "'" & ReadRows.Item("TP_RESOLUCAO") & "'," & _
                                    "'" & ReadRows.Item("USR_ATEND") & "'," & _
                                    "'" & ReadRows.Item("USR_ATEND_PF") & "'," & _
                                    "'" & ReadRows.Item("USR_RESOL") & "'," & _
                                    "'" & ReadRows.Item("USR_RESOL_PF") & "'," & _
                                    "'" & ReadRows.Item("COD_OS") & "'," & _
                                    "'" & ReadRows.Item("OS_DT_ATEND") & "'," & _
                                    "'" & ReadRows.Item("ID_NOTIFICACAO") & "'," & _
                                    "'" & ReadRows.Item("NOT_US_CODIGO") & "'," & _
                                    "'" & ReadRows.Item("NOT_DT_ABERTURA") & "'," & _
                                    "'" & ReadRows.Item("NOT_DT_ENCERRAMENTO") & "'," & _
                                    "'" & ReadRows.Item("NOT_TP_RESOL") & "'," & _
                                    "'" & ReadRows.Item("OBS") & "'," & _
                                    "'" & ReadRows.Item("OBS_RESOL").ToString.Replace("'", " - ") & "'," & _
                                    "'" & ReadRows.Item("NOT_OBS").ToString.Replace("'", " - ") & "')"


                            _SQL = Replace(_SQL, ", ,", ",'',")
                            _SQL = Replace(_SQL, "/", "#")

                            MysqlAdd("IGNORE", "ora_qnet.qnet_slaie", _SQL, "")
                            _SQL = Nothing
                        Next

                        '// DELETA MAIOR QUE 90 DIAS
                        MysqlDEL("ora_qnet.qnet_slaie", "DT_NOTA < CURDATE() - INTERVAL 90 DAY")
                    End Using
                End Using
            End Using
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message & "(" & linha & ")", "SLA IE UPDATE")
        End Try

    End Sub



End Class
