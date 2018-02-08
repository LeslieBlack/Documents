package xml;

import java.io.File;
import java.io.IOException;
import java.math.BigInteger;

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

public class CreateTestOrderXML {

	/**************************************************************************************
	 * Create test orders from existing XML files
	 *  
	 * @param args
	 *************************************************************************************/
	public static void main(String[] args) {


		//final String Source = "C:\\Applications\\XMLFILES\\SOURCE\\SIT DW orders";
		//final String SourcePath = "C:\\Applications\\XMLFILES\\SOURCE\\SIT DW orders\\";
		final String Source = "C:\\Applications\\XMLFILES\\LOADS";
		final String SourcePath = "C:\\Applications\\XMLFILES\\LOADS\\";
		final String Destination =  "C:\\Applications\\XMLFILES\\DEST\\"; 
		
		// Seed the OrderNum object.
		BigInteger orderNum = new BigInteger("5000000000");	
		BigInteger removal = new BigInteger("5000000000");
		
		DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder dBuilder; 

		try {

			File folder = new File(Source);
			File[] listOfFiles = folder.listFiles();
			int i = 0;

			for (File file : listOfFiles) {

				i++;

				if (file.isFile()) { 

					dBuilder = dbFactory.newDocumentBuilder();
					Document doc = dBuilder.parse(new File(SourcePath + file.getName()));
					doc.getDocumentElement().normalize();

					//update order-num attribute value
					orderNum = updateAttributeValue(doc,orderNum);

					//update Element value
					//updateElementValue(doc);
					updateElementValue(doc,"created-by","leslie storefront");
					updateElementValue(doc,"customer-name","Leslie@fatface" );

					// write the updated XML to file 
					doc.getDocumentElement().normalize();
					TransformerFactory transformerFactory = TransformerFactory.newInstance();
					Transformer transformer = transformerFactory.newTransformer();
					DOMSource source = new DOMSource(doc);
					StreamResult result = new StreamResult(new File(Destination + file.getName()));

					transformer.setOutputProperty(OutputKeys.INDENT, "yes");
					transformer.transform(source, result);		
				}  
			} 
            
			orderNum = orderNum.subtract(removal); // removes the original  big int
			System.out.println(orderNum.toString() + " Test XML orders created successfully, across " + String.valueOf(i) +" Files." );

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
	private static BigInteger updateAttributeValue(Document doc,BigInteger OrderNumNumeric) {

		BigInteger increment = new BigInteger("1");
		String OrderAlpha =  "UK";
		NodeList order = doc.getElementsByTagName("order");
		Element orderElement = null;

		//loop for each order
		for(int i=0; i < order.getLength(); i++ ){
			OrderNumNumeric = OrderNumNumeric.add(increment);
			orderElement = (Element) order.item(i);
			orderElement.setAttribute("order-no", OrderAlpha + OrderNumNumeric);				
		}
		
		return OrderNumNumeric;
	}

	/***
	 * 
	 * @param doc
	 */
	private static void updateElementValue(Document doc, String elementToUpdate, String updatevalue) {
		
		NodeList orderNodelist = doc.getElementsByTagName("order");
		Element emp = null;
		
		//loop for each order
		for(int i=0; i < orderNodelist.getLength(); i++){
			emp = (Element) orderNodelist.item(i);
			Node name = emp.getElementsByTagName(elementToUpdate).item(0).getFirstChild();
			//name.setNodeValue(name.getNodeValue().toUpperCase());
			name.setNodeValue(updatevalue);
			
		}
	}	
}
