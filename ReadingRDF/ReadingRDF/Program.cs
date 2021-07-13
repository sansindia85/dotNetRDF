using System;
using System.IO;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Parsing.Handlers;

namespace ReadingRDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //GraphParsers();
            //GraphParsersWithExceptionHandling();
            //ReadingRDFfromFiles();
            //ReadingRDFfromURI();
            //ReadingRDFfromEmbeddedResource();
            //ReadingRDFfromStrings();
            //ReadingRDFfromStringsAlternate();
            StoreParsers();
            //AdvancedParsing();

        }

        static void GraphParsers()
        {            
            TurtleParser turtleParser = new TurtleParser();

            IGraph graph = new Graph();
            //Load using a Filename
            turtleParser.Load(graph, @"Files\helloworld.ttl");

            IGraph streamReaderGraph = new Graph();
            //Load using a StreamReader
            turtleParser.Load(streamReaderGraph, new StreamReader(@"Files\helloworld.ttl"));
        }

        static void GraphParsersWithExceptionHandling()
        {
            try
            {
                IGraph graph = new Graph();
                NTriplesParser nTriplesParser = new NTriplesParser();
                //NTriplesParser nTriplesParser = new NTriplesParser(NTriplesSyntax.Original);
                nTriplesParser.Load(graph, @"Files\helloworld.nt");
            }
            catch (RdfParseException parseEx)
            {
                //This indicates a parser error e.g unexpected character, premature end of input, invalid syntax etc.
                Console.WriteLine("Parser Error");
                Console.WriteLine(parseEx.Message);
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }
        }

        static void ReadingRDFfromFiles()
        {
            IGraph graph = new Graph();
            FileLoader.Load(graph, @"Files\helloworld.rdf");
        }

        static void ReadingRDFfromURI()
        {
            IGraph graph = new Graph();
            UriLoader.Load(graph, new Uri("https://dbpedia.org/page/Barack_Obama"));
        }

        static void ReadingRDFfromEmbeddedResource()
        {
            IGraph graph = new Graph();
            EmbeddedResourceLoader.Load(graph, "ReadingRDF.Files.HelloWorldEmbedded.rdf, ReadingRDF");
        }

        static void ReadingRDFfromStrings()
        {
            IGraph graph = new Graph();
            StringParser.Parse(graph, "<http://example.org/a> <http://example.org/b> <http://example.org/c>.");
        }

        static void ReadingRDFfromStringsAlternate()
        {
            Graph graph = new Graph();
            NTriplesParser parser = new NTriplesParser();
            parser.Load(graph, new StringReader("<http://example.org/a> <http://example.org/b> <http://example.org/c>."));
        }

        static void StoreParsers()
        {
            TripleStore store = new TripleStore();
            TriGParser trigParser = new TriGParser();

            trigParser.Load(store, @"Files\helloworld.trig");
        }

        static void AdvancedParsing()
        {
            CountHandler countHandler = new CountHandler();
            TurtleParser turtleParser = new TurtleParser();
            turtleParser.Load(countHandler, @"Files\helloworld.ttl");

            Console.WriteLine($"Count : {countHandler.Count}");
        }

    }
}
