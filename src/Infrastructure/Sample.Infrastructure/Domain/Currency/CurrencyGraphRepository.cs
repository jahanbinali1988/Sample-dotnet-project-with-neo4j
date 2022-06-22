using Neo4j.Driver;
using Neo4jObjectMapper;
using Sample.Domain.Currency;
using Sample.Domain.Currency.GraphModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Domain.Currency
{
    public class CurrencyGraphRepository : ICurrencyGraphRepository
    {
        private readonly INeoContext _context;
        public CurrencyGraphRepository(INeoContext context)
        {
            this._context = context;
        }

        public async Task AddRateAsync(CurrencyRateGraphModel currencyRate)
        {
            IResultSummary resultSummary = await _context.InsertRelation<CurrencyRateGraphModel>(
                "MATCH (c1:CurrencyGraphModel { Id : " + $"'{currencyRate.OriginCurrencyId}'" + " }) " +
                "MATCH (c2:CurrencyGraphModel { Id: " + $"'{currencyRate.DestinationCurrencyId}'" + " })", "c1", "c2",
                currencyRate
            );
        }

        public async Task CreateAsync(CurrencyGraphModel currency)
        {
            IResultSummary resultExecuting = await _context.InsertNode<CurrencyGraphModel>(currency);
        }

        public async Task<CurrencyDestinationRoute> GetRouteAsync(CurrencyGraphModel originCurrency, CurrencyGraphModel destinationCurrency)
        {
            var query = "MATCH (source:CurrencyGraphModel {Id: " + $"'{originCurrency.Id}'" + "}), (target:CurrencyGraphModel {Id: " + $"'{destinationCurrency.Id}'" + "})" + "\r\n" +
                        "CALL gds.shortestPath.dijkstra.stream('myGraph', {" + "\r\n" +
                            "sourceNode: source," + "\r\n" +
                            "targetNode: target," + "\r\n" +
                            "relationshipWeightProperty: 'cost'" + "\r\n" +
                        "})" + "\r\n" +
                        "YIELD index, sourceNode, targetNode, totalCost, nodeIds, costs, path" + "\r\n" +
                        "RETURN" + "\r\n" +
                            "index," + "\r\n" +
                            "gds.util.asNode(sourceNode).name AS sourceNodeName," + "\r\n" +
                            "gds.util.asNode(targetNode).name AS targetNodeName," + "\r\n" +
                            "totalCost," + "\r\n" +
                            "[nodeId IN nodeIds | gds.util.asNode(nodeId).name] AS nodeNames," + "\r\n" +
                            "costs," + "\r\n" +
                            "nodes(path) as path" + "\r\n" +
                        "ORDER BY index";
            var result = await _context.QueryDefault<CurrencyDestinationRoute>(query);
            return result;
        }
    }
}
