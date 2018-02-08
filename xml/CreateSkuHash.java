package xml;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.Hashtable;

public class CreateSkuHash {

	public static void main(String[] args) {

		final String sku_path =  "C:\\Applications\\XMLFILES\\SKU\\SKU.txt";
	
		try {
			
			Hashtable< Integer, String > 
			hash = new Hashtable< Integer, String >();
			BufferedReader rd = new BufferedReader( new FileReader (sku_path));
			String line;

			int i = 0;

			while ((line = rd.readLine()) != null){
				
				hash.put(i, line);
				i++;
			}
			
			for ( int j = 0 ; j < hash.size() ; j++) {
				System.out.println(String.valueOf(j) + " " + hash.get(j));
			}
			
		} catch (IOException e) {
		
			e.printStackTrace();
		}
	}
		
}
