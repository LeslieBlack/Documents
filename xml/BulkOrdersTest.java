package xml;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.math.BigInteger;
import java.util.Hashtable;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

public class BulkOrdersTest {
	
	/**************************************************************************************
	 * Create test orders from existing XML files
	 * Replacing  
	 * main
	 * @param args
	 *************************************************************************************/
	public static void main(String[] args) {
						
	    final String Source = "C:\\Applications\\XMLFILES\\BulkTest";
		final String SourcePath = "C:\\Applications\\XMLFILES\\BulkTest\\";
		final String Destination =  "C:\\Applications\\XMLFILES\\BulkTest\\Results\\"; 
		final int  NumberOfReads =  200;	
		int TotalOrderCount = 0;
		BigInteger orderNum = new BigInteger("9000000");	
		BigInteger removal = new BigInteger("9000000");	
		//UKFBLK9000000
		//UKF1000589576
		DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder dBuilder; 
		int FilesWritten = 0;
				
		try {

			File folder = new File(Source);
			
			File[] listOfFiles = folder.listFiles();
			int i = 0;
            // Read the Template file n times.
			for(int readindex=1; readindex <= NumberOfReads; readindex++ ){

				for (File file : listOfFiles) {

					i++;

					if (file.isFile()) { 

						dBuilder = dbFactory.newDocumentBuilder();
						Document doc = dBuilder.parse(new File(SourcePath + file.getName()));
						doc.getDocumentElement().normalize();
						NodeList orderNodelist = doc.getElementsByTagName("order");
						TotalOrderCount = TotalOrderCount  + orderNodelist.getLength();
						orderNum = updateAttributeOrderNumValue(doc,orderNum);
						updateElementValue(doc,"created-by","XXXXXX");
						updateElementValue(doc,"customer-name","XXXXXX" );
						updateElementValue(doc,"customer-email","XXXXXX" );
						updateElementValue(doc,"title","XXXXXX");
						updateElementValue(doc,"first-name","XXXXXX" );
						updateElementValue(doc,"last-name","XXXXXX" );
						updateElementValue(doc,"address1","XXXXXX" );
						updateElementValue(doc,"city","XXXXXX" );
						FilesWritten++;
						doc.getDocumentElement().normalize();
						TransformerFactory transformerFactory = TransformerFactory.newInstance();
						Transformer transformer = transformerFactory.newTransformer();
						DOMSource source = new DOMSource(doc);
						//StreamResult result = new StreamResult(new File(Destination + file.getName()));
						StreamResult result = new StreamResult(new File(Destination + "BulkTest" + String.valueOf(readindex) + ".xml"));
						transformer.setOutputProperty(OutputKeys.INDENT, "yes");
						transformer.transform(source, result);							
					}

				}
			}

			orderNum = orderNum.subtract(removal); // Truncates the original  BigInteger			
			System.out.println(String.valueOf(FilesWritten) + " files harvested with provided SKU line items, yielding " + orderNum.toString() + " order(s) from a total of " + String.valueOf(i) +" Files in total " );
			System.out.println("Total orders = " + String.valueOf(TotalOrderCount));

		} catch (SAXException | ParserConfigurationException | IOException | TransformerException e1) {
			e1.printStackTrace();			
		} 
	}

	
	/************************************************************************************
	 * Calculate the Order number based upon the parameter passed.
	 * Loop through the order nodes replacing the order-number attribute
	 * with the calculated order number, which is then returned inn an incremented 
	 * state for use in the next file(s). 
	 *   
	 * @param doc
	 * @param OrderNumNumeric
	 * @return OrderNumNumeric
	 * 
	 ************************************************************************************/
	private static BigInteger updateAttributeOrderNumValue(Document doc,BigInteger OrderNumNumeric) {

		BigInteger increment = new BigInteger("1");
		final String OrderAlpha =  "UKFBLK";
		NodeList order = doc.getElementsByTagName("order"); 
		Element orderElement = null;	
								
		//For each order 
		for(int i=0; i < order.getLength(); i++ ){								
			OrderNumNumeric = OrderNumNumeric.add(increment);	
			orderElement = (Element) order.item(i);					
			orderElement.setAttribute("order-no", OrderAlpha + OrderNumNumeric);
			updateExistingOrderNumsElementValue(doc,"original-order-no",OrderAlpha + OrderNumNumeric,i);
			updateExistingOrderNumsElementValue(doc,"current-order-no",OrderAlpha + OrderNumNumeric,i);	
		}	
		return OrderNumNumeric;
	}
		
	
	/******************************************************************************
	 * updateExistingOrderNumsElementValue
	 * @param doc
	 * @param elementToUpdate
	 * @param updatevalue
	 ******************************************************************************/
	private static void updateExistingOrderNumsElementValue(Document doc, String elementToUpdate, String updatevalue, int occurrence) {
		
		NodeList orderNodelist = doc.getElementsByTagName("order");
		Element order = null;
				
			order = (Element) orderNodelist.item(occurrence);	
			Node name = order.getElementsByTagName(elementToUpdate).item(0).getFirstChild();
			name.setNodeValue(updatevalue);	
	}
	
	/******************************************************************************
	 * updateElementValue
	 * @param doc
	 * @param elementToUpdate
	 * @param updatevalue
	 ******************************************************************************/
	private static void updateElementValue(Document doc, String elementToUpdate, String updatevalue) {
		
		NodeList orderNodelist = doc.getElementsByTagName("order");
		Element order = null;
		
		//For each order
		for(int i=0; i < orderNodelist.getLength(); i++){
			order = (Element) orderNodelist.item(i);	
			Node name = order.getElementsByTagName(elementToUpdate).item(0).getFirstChild();
			name.setNodeValue(updatevalue);	
		}
	}				
}
