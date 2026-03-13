## Endpoints
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | /api/assinantes | Listar assinantes ativos |
| GET | /api/assinantes/{id} | Buscar assinante por ID |
| POST | /api/assinantes | Criar assinante |
| PUT | /api/assinantes/{id} | Atualizar assinante |
| PATCH | /api/assinantes/{id}/desativar | Desativar assinante |
| DELETE | /api/assinantes/{id} | Excluir assinante |

## Regras de Negócio
- Tempo de assinatura calculado dinamicamente em meses
- Tempo de assinatura não pode ser igual a 0
- Data de início não pode ser maior que a data atual
- Valor mensal deve ser maior que 0
- E-mail deve ter formato válido
- E-mail deve ser único no sistema
- Listagem e visualização consideram apenas assinantes ativos
