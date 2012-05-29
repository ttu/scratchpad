<?php
	function getData($url) {
	
		try {
			$ch = curl_init();
			curl_setopt($ch, CURLOPT_URL, $url);
			curl_setopt($ch, CURLOPT_HEADER, false);
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
			$data = curl_exec($ch);
			curl_close($ch);
		} catch (Exception $e) {
			writeErrorLog('cURL threw an exception: '.$e->getMessage());			
			return false;
		}

		return $data;
	}
	
	function writeErrorLog($entry) {
		$date = date("d.m.Y H:i");
		$file = fopen("errorLog.txt", "a"); 
		fwrite($file, "$date: $entry\r\n");
		fclose($file);
	}
	
	$user_agent =  $_SERVER['HTTP_USER_AGENT'];
	$ip = ((isset($_SERVER['HTTP_X_FORWARDED_FOR'])) ? $_SERVER['HTTP_X_FORWARDED_FOR'] : $_SERVER['REMOTE_ADDR']);
	$date = date("d.m.Y H:i");
	$logEntry = "$date IP: $ip - $user_agent\r\n";
	$fileName = "ip_list.txt";
	$isBot = false;
	
	// Personal skip list
	if ($ip == "" || strpos($ip, "00.000") === 0)
		return;

	// Just dummy check if user agent contains bot, it's pretty likely a bot
	if (strpos(strtolower($user_agent), "bot") !== false){
		$isBot = true;
	}
	else {
		$url = 'http://www.useragentstring.com/?getJSON=all&uas='.urlencode($user_agent);
		
		$json = getData($url);

		if ($json != null){
			$obj = json_decode($json);
			if ($obj != null){
				$agent_type = strtolower($obj->{'agent_type'});
				if ($agent_type != "unknown" && $agent_type != "browser")
					$isBot = true;
			}
		}
	}

	if ($isBot)
		$fileName = "ip_bot_list.txt";
		
	$file = fopen($fileName, "a");
	fwrite($file, $logEntry); 
	fclose($file); 
?>