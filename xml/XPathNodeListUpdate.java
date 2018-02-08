package xml;

import java.io.File;
import java.io.IOException;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerConfigurationException;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.TransformerFactoryConfigurationError;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

import org.w3c.dom.Attr;
import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

public class XPathNodeListUpdate {
		
		public static void main(String[] args) {
			
			final String Source = "C:\\Applications\\XMLFILES\\SOURCE\\SIT DW orders";
			final String SourcePath = "C:\\Applications\\XMLFILES\\SOURCE\\SIT DW orders\\";
			final String Destination =  "C:\\Applications\\XMLFILES\\DEST\\"; 
			final String orderDateXpath = "/orders/order/order-date";
			final String orderNumberXpath =  "/orders/order/@order-no";  // NB is a node attribute.
			int orderCount =0;

			try {
						
				File folder = new File(Source);
				File[] listOfFiles = folder.listFiles();
				int i = 0;
				
				for (File file : listOfFiles) {
			    
					i++;
						
					if (file.isFile()) {
						
				    	//System.out.println(file.getName() + " count = " + String.valueOf(i) );
				    	
				    	DocumentBuilderFactory f = DocumentBuilderFactory.newInstance();
						DocumentBuilder b = f.newDocumentBuilder();
						Document doc = b.parse(new File(SourcePath + file.getName()));
			
						XPath xPath = XPathFactory.newInstance().newXPath();
						NodeList orders = (NodeList) xPath.evaluate("/orders/order", doc, XPathConstants.NODESET);
						
							
						
					//System.out.println("Number of orders in file = " +  String.valueOf(orders.getLength()) );
					orderCount = orderCount + orders.getLength();
								
				    	
					
					// transform and write file
					Transformer tf = TransformerFactory.newInstance().newTransformer();
					tf.setOutputProperty(OutputKeys.INDENT, "yes");
					tf.setOutputProperty(OutputKeys.METHOD, "xml");
					tf.setOutputProperty("{http://xml.apache.org/xslt}indent-amount", "4");
			    	   	
					DOMSource domSource = new DOMSource(doc);
					StreamResult sr = new StreamResult(new File(Destination + file.getName()));
					tf.transform(domSource, sr);
					
					
				    } // next file
					
					
				
				
					
				}
				
				System.out.println("total orders = " +  String.valueOf(orderCount)+ " in " + String.valueOf(i) + " Files");
				
				
				/*for (int i2 = 1; i < 5000; i++) {
			    } */
				
		
			} catch (ParserConfigurationException e) {
				e.printStackTrace();
			} catch (SAXException e) {
				e.printStackTrace();
			} catch (IOException e) {	
				e.printStackTrace();
			} catch (XPathExpressionException e) { 	
				e.printStackTrace();
			} catch (TransformerConfigurationException e) {	
				e.printStackTrace();
			} catch (TransformerFactoryConfigurationError e) {
				e.printStackTrace();
			} catch (TransformerException e) {
				e.printStackTrace();                                        
			}
				
		}  


	}
