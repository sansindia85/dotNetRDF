using System;
using VDS.RDF;
using VDS.RDF.Storage;
using VDS.RDF.Writing;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //First thing we want to do is create a graph in which we do as follows:
            IGraph graph = new Graph();

            //Notice that we use the Create() method of the UriFactory class to create URIs
            //since this takes advantage of dotNetRDF's URI interning feature to reduce memory
            //usage and speed up equality comparisons on URIs.
            IUriNode dotNetRDF = graph.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            
            IUriNode says = graph.CreateUriNode(UriFactory.Create("http://example.org/says"));

            ILiteralNode helloWorld = graph.CreateLiteralNode("Hello World");
            ILiteralNode bonjourMonde = graph.CreateLiteralNode("Bonjour tout le Monde", "fr");

            graph.Assert(new Triple(dotNetRDF, says, helloWorld));
            graph.Assert(new Triple(dotNetRDF, says, bonjourMonde));

            //Those of you who know RDF will notice that the above is not a valid RDF syntax - this
            //is just an ultra simple representation of the Triples and is primarily intended for
            //debugging.
            foreach (Triple triple in graph.Triples)
            {
                Console.WriteLine(triple.ToString());
            }
            
            //If we want to actually output RDF syntax then we need to use one of the classes from the
            //VDS.RDF.Writing namespace.First we'll output the above in NTriples syntax.

            NTriplesWriter nTriplesWriter = new NTriplesWriter();
            nTriplesWriter.Save(graph, "HelloWorld.nt");

            //If we want to save the Graph to another RDF syntax we could do that as well e.g.
            RdfXmlWriter rdfXmlWriter = new RdfXmlWriter();
            rdfXmlWriter.Save(graph, "HelloWorld.rdf");

            SparqlHttpProtocolConnector sparql = new SparqlHttpProtocolConnector(new Uri("http://localhost:3030/helloworld"));
            sparql.SaveGraph(graph);
        }
    }
}
